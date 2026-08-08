using Application.Abstractions.Jobs;
using Hangfire;
using Hangfire.Redis.StackExchange;
using JobService.Jobs;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace JobService;

public static class ConfigureServices
{
    public static IServiceCollection AddJobService(this IServiceCollection services)
    {
        services.AddHangfire((provider, config) =>
        {
            var redis = provider.GetRequiredService<IConnectionMultiplexer>();

            config.UseRedisStorage(redis, new RedisStorageOptions()
            {
                Prefix = "hangfire:",
            });
        });
        
        services.AddHangfireServer();

        services.AddScoped<ListingExpirationJob>();

        services.AddScoped<IJobService, JobService>();
        
        return services;
    }
}