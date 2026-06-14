using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureService).Assembly;

        /*** FluentValidator ***/
        services.AddValidatorsFromAssembly(assembly);
        
        /*** MediatR ***/
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        
        return services;
    }
}