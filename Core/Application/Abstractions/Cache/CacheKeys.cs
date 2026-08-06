namespace Application.Abstractions.Cache;

internal static class CacheKeys
{
    public const string CategoriesAll = "Categories:all";

    public static string ListingsByCategoryId(Guid categoryId)
    {
        return $"listings:by-category-id:{categoryId}";
    }

    public static string ListingByCategoryIdPaginated(Guid categoryId, int pageNumber, int pageSize)
    {
        return $"{ListingsByCategoryId(categoryId)}:page:{pageNumber}:size:{pageSize}";
    }
}