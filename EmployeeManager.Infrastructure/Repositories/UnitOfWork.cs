using AutoMapper;
using EmployeeManager.Core.Interfaces;
using EmployeeManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IPositionRepository PositionRepository { get; }

    public IEmployeeRepository EmployeeRepository { get; }
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        PositionRepository = new PositionRepository(_context);
        EmployeeRepository = new EmployeeRepository(_context);
    }
}

