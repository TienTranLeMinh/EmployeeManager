using AutoMapper;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Viewmodels.Positions;
using EmployeeManager.Core.Entities;
using EmployeeManager.Core.Interfaces;
using System.Reflection;

namespace EmployeeManager.Application.Services;
public class PositionService : IPositionService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PositionService(IUnitOfWork Uow, IMapper mapper)
    {
        _uow = Uow;
        _mapper = mapper;
    }

    public async Task<long?> CreatePosition(PositionCreateModel model)
    {
        try
        {
            var position = _mapper.Map<Position>(model);
            var id = await _uow.PositionRepository.AddAsync(position);
            return id;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> DeletePosition(long id)
    {
        try
        {
            var result = await _uow.PositionRepository.DeleteAsync(id);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<PositionViewModel>> GetAllPosition()
    {
        try
        {
            var positions = await _uow.PositionRepository.GetAllAsync();
            var positionModels = _mapper.Map<List<PositionViewModel>>(positions);

            return positionModels;
        }
        catch (Exception)
        {
            return new List<PositionViewModel>();
        }
    }

    public async Task<PositionViewModel> GetPosition(long id)
    {
        try
        {
            var position = await _uow.PositionRepository.GetByIdAsync(id);
            if (position == null)
            {
                return null;
            }

            var positionModel = _mapper.Map<PositionViewModel>(position);
            return positionModel;
        }
        catch (Exception)
        {

            return null;
        }
    }

    public async Task<long?> UpdatePosition(PositionUpdateModel model)
    {
        try
        {
            var position = await _uow.PositionRepository.GetByIdAsync(model.Id);
            if (position == null)
            {
                return null;
            }

            position.Name = model.Name;
            var id = await _uow.PositionRepository.UpdateAsync(model.Id, position);
            return id;
        }
        catch (Exception)
        {

            return null;
        }
    }
}

