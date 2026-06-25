using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Persistence;
using Respawn;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Api.IntegrationTests;

public class ApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:18-alpine")
        .WithDatabase("test_db")
        .WithUsername("user")
        .WithPassword("password")
        .WithCleanUp(true)
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder("redis:8-alpine")
        .WithCleanUp(true)
        .Build();
    
    private NpgsqlConnection _dbConnection = null!;
    private Respawner _respawner = null!;
    
    public async Task InitializeAsync()
    {
        await Task.WhenAll(
            _dbContainer.StartAsync(), 
            _redisContainer.StartAsync()
        );

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        await db.Database.MigrateAsync();

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await _dbConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }
    
    public new async Task DisposeAsync()
    {
        await _dbConnection.CloseAsync();
        await _dbContainer.StopAsync();
        await _redisContainer.StopAsync();
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((_, cfg) =>
            cfg.AddInMemoryCollection([
                new KeyValuePair<string, string?>("ConnectionStrings:PSQL", _dbContainer.GetConnectionString())
                // TODO redis connection string
            ]));
        
        // TODO add Redis configuration
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
    
    // TODO ResetRedisAsync
    // public async Task ResetCacheAsync()
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<TEntity[]> SeedEntity<TEntity>(params TEntity[] entities) where TEntity : class
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        await db.Set<TEntity>().AddRangeAsync(entities);
        await db.SaveChangesAsync();
        
        return entities;
    }
}