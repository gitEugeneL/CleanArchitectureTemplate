using Domain.Abstractions.Repositories;
using Domain.Exceptions.Listing;
using MediatR;

namespace Application.UseCases.Listings.Queries.GetById;

internal class GetByIdHandler(
    IListingRepository listingRepository
) : IRequestHandler<GetByIdQuery, ListingResponse>
{
    public async Task<ListingResponse> Handle(GetByIdQuery query, CancellationToken ct)
    {
        var listing = await listingRepository.GetByIdAsync(query.ListingId, ct)
            ?? throw new ListingNotFoundException(query.ListingId);

        return listing.ToListingResponse();

    }
}