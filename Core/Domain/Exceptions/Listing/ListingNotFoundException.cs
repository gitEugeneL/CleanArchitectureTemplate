using Domain.Exceptions.Common;

namespace Domain.Exceptions.Listing;

public sealed class ListingNotFoundException(Guid listingId) :
    NotFoundException($"Listing with id: {listingId} not found.");