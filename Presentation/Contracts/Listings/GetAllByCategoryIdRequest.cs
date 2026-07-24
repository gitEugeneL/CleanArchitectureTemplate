using Contracts.Common;

namespace Contracts.Listings;

public sealed record GetAllByCategoryIdRequest(
    int PageNumber = 1,
    int PageSize = 5
) : PaginationParams(PageNumber, PageSize);