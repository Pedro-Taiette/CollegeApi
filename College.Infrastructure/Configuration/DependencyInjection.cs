using College.Domain.Abstractions;
using College.Infrastructure.Data;
using College.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace College.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CollegeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); // DB Injct.
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}