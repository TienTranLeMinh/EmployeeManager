using EmployeeManager.Application.Viewmodels.Positions;

namespace EmployeeManager.Application.Interfaces;
public interface IPositionService
{
    Task<List<PositionViewModel>> GetAllPosition();
    Task<long?> CreatePosition(PositionCreateModel model);
    Task<long?> UpdatePosition(PositionUpdateModel model);
    Task<PositionViewModel> GetPosition(long id);
    Task<bool> DeletePosition(long id);
}

