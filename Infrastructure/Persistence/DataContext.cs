using Domain.Abstractions;
using Domain.Entities.Categories;
using Domain.Entities.Common;
using Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

internal sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Listing> Listings { get; init; }
    public DbSet<Category> Categories { get; init; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.Updated = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}