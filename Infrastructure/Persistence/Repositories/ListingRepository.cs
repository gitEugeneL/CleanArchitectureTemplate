using Domain.Abstractions.Repositories;
using Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class ListingRepository(DataContext dataContext) : IListingRepository
{
    public async Task<Listing> AddAsync(Listing listing, CancellationToken ct)
    {
        await dataContext
            .Listings
            .AddAsync(listing, ct);
        return listing;
    }

    public async Task<Listing?> GetByIdAsync(Guid listingId, CancellationToken ct)
    {
        return await dataContext
            .Listings
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == listingId, ct);
    }

    public async Task<(IReadOnlyList<Listing> List, int Count)> GetAllByCategoryIdAsync(
        Guid categoryId, 
        int pageNumber = 1, 
        int pageSize = 10, 
        CancellationToken ct = default)
    {
        var query = dataContext
            .Listings
            .Where(listing => listing.CategoryId == categoryId)
            .AsNoTracking();

        var count = await query.CountAsync(ct);
        
        var listings = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        
        return (listings, count);
    }
}