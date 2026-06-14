using Domain.Abstractions.Repositories;
using MediatR;

namespace Application.UseCases.Categories.Queries.GetAll;

public class GetAllHandler(
    ICategoryRepository categoryRepository
) : IRequestHandler<GetAllQuery, IReadOnlyList<CategoryResponse>>
{
    public async Task<IReadOnlyList<CategoryResponse>> Handle(GetAllQuery query, CancellationToken ct)
    {
        // TODO check cache 

        var result = (await categoryRepository.GetAllAsync(ct))
            .Select(category => category.ToCategoryResponse())
            .ToList();
        
        // TODO add list (CategoryResponse) to cache 
        
        return result;
    }
}