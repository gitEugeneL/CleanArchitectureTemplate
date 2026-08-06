using Application.Abstractions.Cache;
using Domain.Abstractions.Repositories;
using MediatR;

namespace Application.UseCases.Categories.Queries.GetAll;

public class GetAllHandler(
    ICategoryRepository categoryRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllQuery, IReadOnlyList<CategoryResponse>>
{
    public async Task<IReadOnlyList<CategoryResponse>> Handle(GetAllQuery query, CancellationToken ct)
    {
        var cachedResult = await cacheService.GetAsync<IReadOnlyList<CategoryResponse>>(CacheKeys.CategoriesAll);
        if (cachedResult is not null)
            return cachedResult;
        
        var result = (await categoryRepository.GetAllAsync(ct))
            .Select(category => category.ToCategoryResponse())
            .ToList();
        
        await cacheService.SetAsync(CacheKeys.CategoriesAll, result);
        
        return result;
    }
}