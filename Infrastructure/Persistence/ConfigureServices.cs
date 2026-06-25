using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration config)
    {
        /*** Postgres ***/
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(config.GetConnectionString("PSQL")));
        
        /*** Unit of Work ***/
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<DataContext>());
        
        /*** Repositories ***/
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IListingRepository, ListingRepository>();
        
        return services;
    }
}