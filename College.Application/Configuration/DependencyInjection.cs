using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace College.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Registro Automático de todos os Services - Terimnam com "Service"
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface && !t.IsAbstract);

        foreach (var type in serviceTypes)
        {
            services.AddScoped(type);
        }

        return services;
    }
}