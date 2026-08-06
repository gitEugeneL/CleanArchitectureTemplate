using Application.Abstractions.Cache;
using Application.UseCases.Common;
using Domain.Abstractions.Repositories;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Listings.Queries.GetAllByCategoryId;

internal class GetAllByCategoryIdHandler(
    IListingRepository listingRepository,
    IValidator<GetAllByCategoryIdQuery> validator,
    ICacheService cacheService
) : IRequestHandler<GetAllByCategoryIdQuery, PaginationResult<ListingResponse>>
{
    public async Task<PaginationResult<ListingResponse>> Handle(GetAllByCategoryIdQuery query, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(query, ct);
        
        var cacheKey = CacheKeys.ListingByCategoryIdPaginated(query.CategoryId, query.PageNumber, query.PageSize);
        
        var cachedResult = await cacheService.GetAsync<PaginationResult<ListingResponse>>(cacheKey);
        if (cachedResult is not null)
            return cachedResult;
        
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
        
        await cacheService.SetAsync(cacheKey, response);
        
        return response;
    }
}