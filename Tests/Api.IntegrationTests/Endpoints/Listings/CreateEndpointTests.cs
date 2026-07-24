using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.TestData;
using Application.UseCases.Listings;
using Contracts.Listings;
using Domain.Exceptions.Category;

namespace Api.IntegrationTests.Endpoints.Listings;

public class CreateEndpointTests(ApplicationFactory factory) : IntegrationTestBase(factory)
{
    [Theory]
    [ClassData(typeof(ListingsData))]
    public async Task Create_WithValidDataAndValidCategoryId_ReturnsCreatedResultAndResponse(TestListing testListing)
    {
        // Arrange
        var category = CategoriesData
            .Categories
            .OrderBy(_ => Random.Shared.Next())
            .First();
        await Factory.SeedEntity(category);

        var request = new CreateListingRequest(
            testListing.Title,
            testListing.Description,
            testListing.Price,
            category.Id
        );

        // Act
        var response = await Client.PostAsJsonAsync("/listings", request);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ListingResponse>();
        Assert.NotNull(result);
        
        Assert.Equal(testListing.Title, result.Title);
        Assert.Equal(testListing.Description, result.Description);
        Assert.Equal(testListing.Price, result.Price);
    }

    [Theory]
    [ClassData(typeof(ListingsData))]
    public async Task Create_WithInvalidCategoryId_ReturnsNotFoundResult(TestListing testListing)
    {
        // Arrange
        var invalidCategoryId = Guid.NewGuid();
        var expectedMessage = new CategoryNotFoundException(invalidCategoryId).Message;
        
        var request = new CreateListingRequest(
            testListing.Title,
            testListing.Description,
            testListing.Price,
            invalidCategoryId
        );
        
        // Act
        var response = await Client.PostAsJsonAsync("/listings", request);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var result = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedMessage, result);
    }

    [Theory]
    [ClassData(typeof(ListingsData))]
    public async Task Create_WithInvalidData_ReturnsBadRequestResult(TestListing testListing)
    {
        // Arrange
        var request = new CreateListingRequest(
            string.Empty,
            testListing.Description,
            -1,
            CategoriesData.Categories.First().Id
        );
        
        // Act
        var response = await Client.PostAsJsonAsync("/listings", request);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}