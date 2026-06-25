using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.TestData;
using Application.UseCases.Listings;
using Domain.Exceptions.Listing;

namespace Api.IntegrationTests.Endpoints.Listings;

public class GetByIdEndpointTest(ApplicationFactory factory) : IntegrationTestBase(factory)
{
    [Theory]
    [ClassData(typeof(ListingsData))]
    public async Task GetById_ReturnsOkResultAndResponse(TestListing testListing)
    {
        // Arrange
        var category = CategoriesData
            .Categories
            .OrderBy(_ => Random.Shared.Next())
            .First();
        await Factory.SeedEntity(category);
        
        var listing = ListingsData.CreateListing(testListing, category.Id);
        await Factory.SeedEntity(listing);
        
        // Act
        var response = await Client.GetAsync($"/listings/{listing.Id}");
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ListingResponse>();
        Assert.NotNull(result);
        
        Assert.Equal(listing.Id, result.ListingId);
        Assert.Equal(listing.Title, result.Title);
        Assert.Equal(listing.Description, result.Description);
        Assert.Equal(listing.Price, result.Price);
        Assert.Equal(listing.Status.ToString(), result.Status);
    }

    [Theory]
    [ClassData(typeof(InvalidIds))]
    public async Task GetById_ReturnsNotFoundResultWhenListingDoesNotExist(Guid invalidListingId)
    {
        // Arrange
        var expectedMessage = new ListingNotFoundException(invalidListingId).Message;
        
        // Act
        var response = await Client.GetAsync($"/listings/{invalidListingId}");
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var result = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedMessage, result);
    }
}