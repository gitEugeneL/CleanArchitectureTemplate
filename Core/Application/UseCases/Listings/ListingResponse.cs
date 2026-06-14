namespace Application.UseCases.Listings;

public sealed record ListingResponse(
    Guid ListingId,
    string Title,
    string Description,
    decimal Price,
    string Status,
    DateTime? ClosedDateTime
);