using Domain.Entities.Listings;

namespace Domain.Abstractions.Repositories;

public interface IListingRepository
{
    Task<Listing> AddAsync(Listing listing, CancellationToken ct);
    
    Task<Listing?> GetByIdAsync(Guid listingId, CancellationToken ct);
    
    Task<Listing?> GetByIdWithTrackingAsync(Guid listingId, CancellationToken ct);
    
    Task<(IReadOnlyList<Listing> List, int Count)> GetAllByCategoryIdAsync(
        Guid categoryId, 
        int pageNumber, 
        int pageSize, 
        CancellationToken ct);
}