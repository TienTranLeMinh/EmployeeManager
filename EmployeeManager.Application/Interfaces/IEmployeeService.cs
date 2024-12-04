using EmployeeManager.Application.Viewmodels;
using EmployeeManager.Application.Viewmodels.Employees;
using EmployeeManager.Core.Entities;

namespace EmployeeManager.Application.Interfaces;
public interface IEmployeeService
{
    Task<List<EmployeeViewModel>> GetAllEmployee();
    Task<string> CreateEmployee(EmployeeCreateModel model);
    Task<string> UpdateEmployee(EmployeeUpdateModel model);
    Task<EmployeeViewModel> GetEmployee(string id);
    Task<bool> DeleteEmployee(string id);
    Task<PaginatedViewModel<EmployeeViewModel>> GetPagedAsync(int pageIndex, int pageSize);
}

