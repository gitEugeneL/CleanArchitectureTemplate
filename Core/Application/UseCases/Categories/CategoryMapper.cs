using Domain.Entities.Categories;

namespace Application.UseCases.Categories;

public static class CategoryMapper
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse(category.Id, category.Name);
    }
}