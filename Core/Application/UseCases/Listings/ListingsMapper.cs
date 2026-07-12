using Application.UseCases.Listings.Commands.Create;
using Domain.Entities.Listings;

namespace Application.UseCases.Listings;

public static class ListingsMapper
{
    public static ListingResponse ToListingResponse(this Listing listing)
    {
        return new ListingResponse(
            listing.Id,
            listing.Title,
            listing.Description,
            listing.Price,
            listing.Status.ToString(),
            listing.ClosedDateTime
        );
    }

    public static Listing ToListing(this CreateCommand command)
    {
        return new Listing(
            command.Title, 
            command.Description, 
            command.Price, 
            command.CategoryId
        );
    }
}