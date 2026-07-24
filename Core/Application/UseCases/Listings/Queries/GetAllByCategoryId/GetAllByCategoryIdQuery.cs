using Application.UseCases.Common;
using MediatR;

namespace Application.UseCases.Listings.Queries.GetAllByCategoryId;

public sealed record GetAllByCategoryIdQuery(
    Guid CategoryId,
    int PageNumber,
    int PageSize
) : PaginationQuery(PageNumber, PageSize), IRequest<PaginationResult<ListingResponse>>;