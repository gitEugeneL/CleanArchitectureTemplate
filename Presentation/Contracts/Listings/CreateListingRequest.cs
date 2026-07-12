namespace Contracts.Listings;

public sealed record CreateListingRequest(
    string Title, 
    string Description, 
    decimal Price, 
    Guid CategoryId
);