using Bogus;
using Domain.Entities.Categories;
using Domain.Entities.Listings;

namespace Persistence;

internal static class DataGenerator
{
    public static void Seed(DataContext dataContext)
    {
        if (dataContext.Categories.Any())
            return;

        var categories = new Faker<Category>().CustomInstantiator(f => new Category(
                f.Commerce.Department().Split(',').First().Trim() + f.IndexFaker,
                f.Commerce.ProductDescription()))
            .Generate(10);
        
        dataContext.Categories.AddRange(categories);
        dataContext.SaveChanges();
        
        var listings = new Faker<Listing>().CustomInstantiator(f => new Listing(
            f.Commerce.ProductName(),
            f.Commerce.ProductDescription(),
            f.Random.Decimal(10, 10000),
            f.PickRandom(categories).Id))
            .Generate(300);
        
        dataContext.Listings.AddRange(listings);
        dataContext.SaveChanges();
    }
}