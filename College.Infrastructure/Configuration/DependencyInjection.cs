using College.Domain.Abstractions;
using College.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace College.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}