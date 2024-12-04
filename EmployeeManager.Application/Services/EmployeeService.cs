using AutoMapper;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Viewmodels;
using EmployeeManager.Application.Viewmodels.Employees;
using EmployeeManager.Core.Entities;
using EmployeeManager.Core.Interfaces;
using System.Globalization;

namespace EmployeeManager.Application.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork Uow, IMapper mapper)
    {
        _uow = Uow;
        _mapper = mapper;
    }

    public async Task<string> CreateEmployee(EmployeeCreateModel model)
    {
        try
        {
            var employee = _mapper.Map<Employee>(model);
            var id = await _uow.EmployeeRepository.AddAsync(employee);
            return id.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> DeleteEmployee(string id)
    {
        try
        {
            if (!Guid.TryParse(id, out Guid _guid))
            {
                return false;
            }
            var result = await _uow.EmployeeRepository.DeleteAsync(_guid);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<EmployeeViewModel>> GetAllEmployee()
    {
        try
        {
            var employees = await _uow.EmployeeRepository.GetAllAsync(x => x.Position);
            var employeeModels = _mapper.Map<List<EmployeeViewModel>>(employees);

            return employeeModels;
        }
        catch (Exception)
        {
            return new List<EmployeeViewModel>();
        }
    }

    public async Task<EmployeeViewModel> GetEmployee(string id)
    {
        try
        {
            if (!Guid.TryParse(id, out Guid _guid))
            {
                return null;
            }

            var employee = await _uow.EmployeeRepository.GetByIdAsync(_guid, x => x.Position);
            if (employee == null)
            {
                return null;
            }

            var employeeModel = _mapper.Map<EmployeeViewModel>(employee);
            return employeeModel;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PaginatedViewModel<EmployeeViewModel>> GetPagedAsync(int pageIndex, int pageSize)
    {
        try
        {
            var result = await _uow.EmployeeRepository.GetPagedAsync(pageIndex, pageSize, x => x.Position);
            if (result.Entities.Any())
            {
                return new PaginatedViewModel<EmployeeViewModel>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = result.TotalCount,
                    Data = _mapper.Map<List<EmployeeViewModel>>(result.Entities),
                };
            }
            return new PaginatedViewModel<EmployeeViewModel>();
        }
        catch (Exception)
        {

            return new PaginatedViewModel<EmployeeViewModel>();
        }
    }

    public async Task<string> UpdateEmployee(EmployeeUpdateModel model)
    {
        try
        {
            if (!Guid.TryParse(model.Id, out Guid _guid))
            {
                return null;
            }

            var employee = await _uow.EmployeeRepository.GetByIdAsync(_guid);
            if (employee == null)
            {
                return null;
            }

            var position = await _uow.PositionRepository.GetByIdAsync(model.PositionId);
            if (position == null)
            {
                return null;
            }

            employee.Name = model.Name;
            employee.PositionId = model.PositionId;
            employee.BirthDate = DateTimeOffset.ParseExact(
                model.BirthDate,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal
                );

            var id = await _uow.EmployeeRepository.UpdateAsync(_guid, employee);
            return id.ToString();
        }
        catch (Exception)
        {

            return null;
        }
    }
}

