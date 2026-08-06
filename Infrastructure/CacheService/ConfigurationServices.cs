using Application.Abstractions.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CacheService;

public static class ConfigurationServices
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")!));

        services.AddSingleton<ICacheService, CacheService>();
        
        return services;
    }
}