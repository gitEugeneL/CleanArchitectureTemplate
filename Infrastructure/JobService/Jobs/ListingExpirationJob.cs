using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Enums;

namespace JobService.Jobs;

internal class ListingExpirationJob(
    IListingRepository listingRepository,
    IUnitOfWork unitOfWork)
{
    public async Task Execute(Guid listingId, CancellationToken ct = default)
    {
        var listing = await listingRepository.GetByIdWithTrackingAsync(listingId, ct);
        if (listing is null || listing.Status == Status.Closed)
            return;
        
        listing.Close();
        await unitOfWork.SaveChangesAsync(ct);
    }
}