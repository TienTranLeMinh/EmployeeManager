namespace EmployeeManager.Core.Interfaces;
public interface IUnitOfWork
{
    public IPositionRepository PositionRepository { get; }

    public IEmployeeRepository EmployeeRepository { get; }
}

