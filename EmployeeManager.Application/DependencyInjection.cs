using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using EmployeeManager.Application.Viewmodels;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddAutoMapper(typeof(MappingViewModel));
        return services;
    }
}

