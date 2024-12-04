using AutoMapper;
using EmployeeManager.Core.Entities;
using EmployeeManager.Core.Interfaces;
using EmployeeManager.Infrastructure.Data;

namespace EmployeeManager.Infrastructure.Repositories;
public class PositionRepository : GenericRepository<Position, long>, IPositionRepository
{
    private readonly ApplicationDbContext _context;

    public PositionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}

