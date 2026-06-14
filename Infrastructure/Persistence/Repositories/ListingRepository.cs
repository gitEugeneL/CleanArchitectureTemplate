using Domain.Abstractions.Repositories;
using Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class ListingRepository(DataContext dataContext) : IListingRepository
{
    public Task<Listing> AddAsync(Listing listing, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Listing?> GetByIdAsync(Guid listingId, CancellationToken ct)
    {
        return await dataContext
            .Listings
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == listingId, ct);
    }

    public Task<(IReadOnlyList<Listing> List, int Count)> GetAllByCategoryIdAsync(
        Guid categoryId, 
        int pageNumber, 
        int pageSize, 
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}