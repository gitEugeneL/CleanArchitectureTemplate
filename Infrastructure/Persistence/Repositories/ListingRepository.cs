using Domain.Abstractions.Repositories;
using Domain.Entities.Listings;

namespace Persistence.Repositories;

internal class ListingRepository(DataContext dataContext) : IListingRepository
{
    public Task<Listing> AddAsync(Listing listing, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Listing?> GetByIdAsync(Guid listingId, CancellationToken ct)
    {
        throw new NotImplementedException();
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