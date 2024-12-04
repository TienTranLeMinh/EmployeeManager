using EmployeeManager.Core.Entities;
using EmployeeManager.Core.Interfaces;
using EmployeeManager.Infrastructure.Data;

namespace EmployeeManager.Infrastructure.Repositories;
public class EmployeeRepository : GenericRepository<Employee, Guid>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}

