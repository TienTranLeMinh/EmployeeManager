using EmployeeManager.Core.Entities;

namespace EmployeeManager.Core.Interfaces;
public interface IEmployeeRepository : IGenericRepository<Employee, Guid>
{
}

