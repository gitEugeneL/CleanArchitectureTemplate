using Domain.Exceptions.Common;

namespace Domain.Exceptions.Listing;

public sealed class ListingAlreadyClosedException(Guid listingId) 
    : ConflictException($"Listing with id: {listingId} is already closed.");