using System.Collections;
using Domain.Entities.Listings;

namespace Api.IntegrationTests.TestData;

public record TestListing(string Title, string Description, decimal Price);

public class ListingsData : IEnumerable<object[]>
{
    public static readonly List<TestListing> TestListings =
    [
        new("Laptop Dell XPS 15", "High performance laptop with 16GB RAM", 1299.99m),
        new("iPhone 14 Pro", "Latest Apple smartphone with 256GB storage", 999.00m),
        new("Gaming Chair", "Ergonomic gaming chair with lumbar support", 299.50m),
        new("Wireless Headphones", "Noise cancelling Bluetooth headphones", 149.99m),
        new("4K Monitor", "27 inch 4K UHD monitor with HDR support", 449.00m)
    ];
    
    
    public IEnumerator<object[]> GetEnumerator()
    {
        return TestListings.Select(tl => (object[])[tl]).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Listing CreateListing(TestListing testListing, Guid categoryId)
    {
        return new Listing(testListing.Title, testListing.Description, testListing.Price, categoryId);
    }
}