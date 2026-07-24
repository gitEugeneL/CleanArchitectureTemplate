using Application.UseCases.Common;
using Domain.Abstractions.Repositories;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Listings.Queries.GetAllByCategoryId;

internal class GetAllByCategoryIdHandler(
    IListingRepository listingRepository,
    IValidator<GetAllByCategoryIdQuery> validator
) : IRequestHandler<GetAllByCategoryIdQuery, PaginationResult<ListingResponse>>
{
    public async Task<PaginationResult<ListingResponse>> Handle(GetAllByCategoryIdQuery query, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(query, ct);
        
        // TODO check cache
        
        // TODO check bd
        var (listings, count) = await listingRepository
            .GetAllByCategoryIdAsync(query.CategoryId, query.PageNumber, query.PageSize, ct);

        var response = new PaginationResult<ListingResponse>(
            listings
                .Select(listing => listing.ToListingResponse())
                .ToList(),
            count,
            query.PageNumber,
            query.PageSize
        );
        
        // TODO add to cache
        
        return response;
    }
}