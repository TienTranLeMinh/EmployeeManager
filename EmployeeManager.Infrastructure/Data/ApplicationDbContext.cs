using EmployeeManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeManager.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Position> Positions { set; get; }

    public virtual DbSet<Employee> Employees { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity<long>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<BaseEntity<Guid>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }

        HandleCodeGeneration();

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    private void HandleCodeGeneration()
    {
        GenerateEmployeeCodes();
        GeneratePositionCodes();
    }

    private void GenerateEmployeeCodes()
    {
        var newEmployees = ChangeTracker.Entries<Employee>()
            .Where(e => e.State == EntityState.Added && string.IsNullOrEmpty(e.Entity.Code))
            .Select(e => e.Entity);

        foreach (var employee in newEmployees)
        {
            var today = DateTime.Now;
            var datePart = $"{today:yyyy_MM_dd}";
            var sequenceNumber = Employees.Count(e => EF.Functions.Like(e.Code, $"NV_{datePart}%")) + 1;
            employee.Code = $"NV_{datePart}_{sequenceNumber}";
        }
    }

    private void GeneratePositionCodes()
    {
        var newPositions = ChangeTracker.Entries<Position>()
            .Where(e => e.State == EntityState.Added && string.IsNullOrEmpty(e.Entity.Code))
            .Select(e => e.Entity);

        foreach (var position in newPositions)
        {
            var sequenceNumber = Positions.Count() + 1;
            position.Code = $"POS_{sequenceNumber}";
        }
    }
}

