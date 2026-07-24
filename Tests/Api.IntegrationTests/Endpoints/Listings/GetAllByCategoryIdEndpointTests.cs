using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.TestData;
using Application.UseCases.Common;
using Application.UseCases.Listings;
using Contracts.Listings;
using Domain.Entities.Categories;

namespace Api.IntegrationTests.Endpoints.Listings;

public class GetAllByCategoryIdEndpointTests(ApplicationFactory factory) : IntegrationTestBase(factory)
{
    [Theory]
    [ClassData(typeof(CategoriesData))]
    public async Task GetAllByCategoryId_WithValidCategoryIdAndValidPaginator_ReturnsOkResultAndResponse(
        Category category)
    {
        // Arrange
        await Factory.SeedEntity(CategoriesData.Categories.ToArray());

        foreach (var testListing in ListingsData.TestListings)
            await Factory.SeedEntity(ListingsData.CreateListing(testListing, category.Id));

        const int pageNumber = 1;
        const int pageSize = 5;
        var totalItems = ListingsData.TestListings.Count;
        var expectedItemsCount = Math.Min(pageSize, totalItems);
        
        // Act
        var response = await Client.GetAsync($"categories/{category.Id}/listings" +
                                             $"?PageNumber={pageNumber}&PageSize={pageSize}");
        
        // Assert 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var result = await response.Content.ReadFromJsonAsync<PaginationResult<ListingResponse>>();
        Assert.NotNull(result);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(totalItems, result.TotalItems);
        Assert.Equal(expectedItemsCount, result.Items.Count);
    }

    [Theory]
    [ClassData(typeof(CategoriesData))]
    public async Task GetAllByCategoryId_WithValidCategoryIdAndWithDefaultPaginator_ReturnsOkResultAndResponse(
        Category category)
    {
        // Arrange
        await Factory.SeedEntity(CategoriesData.Categories.ToArray());
        
        foreach (var testListing in ListingsData.TestListings)
            await Factory.SeedEntity(ListingsData.CreateListing(testListing, category.Id));
        
        var defaultRequest = new GetAllByCategoryIdRequest();
        
        // Act
        var response = await Client.GetAsync($"categories/{category.Id}/listings");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var result = await response.Content.ReadFromJsonAsync<PaginationResult<ListingResponse>>();
        Assert.NotNull(result);
        
        Assert.True(result.Items.Count <= defaultRequest.PageSize);
        Assert.Equal(defaultRequest.PageSize, result.PageSize);
        Assert.Equal(defaultRequest.PageNumber, result.PageNumber);
        Assert.Equal(ListingsData.TestListings.Count, result.TotalItems);
    }

    [Fact]
    public async Task GetAllByCategoryId_WithInvalidCategoryId_ReturnsOkResultAndResponse()
    {
        // Arrange
        var defaultRequest = new GetAllByCategoryIdRequest();
        
        // Act
        var response = await Client.GetAsync($"categories/{Guid.NewGuid()}/listings");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<PaginationResult<ListingResponse>>();
        Assert.NotNull(result);
        
        Assert.Empty(result.Items);
        Assert.Equal(defaultRequest.PageSize, result.PageSize);
        Assert.Equal(defaultRequest.PageNumber, result.PageNumber);
        Assert.Equal(0, result.TotalItems);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    [InlineData(1, 100000)]
    public async Task GetAllByCategoryId_WithInvalidPaginator_ReturnsBadRequestResult(int pageNumber, int pageSize)
    {
        // Act
        var response = await Client.GetAsync($"categories/{Guid.NewGuid()}/listings" +
                                             $"?PageNumber={pageNumber}&PageSize={pageSize}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
}