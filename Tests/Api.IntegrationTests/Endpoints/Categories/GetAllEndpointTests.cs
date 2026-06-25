using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.TestData;
using Application.UseCases.Categories;

namespace Api.IntegrationTests.Endpoints.Categories;

public class GetAllEndpointTests(ApplicationFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task GetAll_ReturnsOkResultAndListOfCategories()
    {
        // Arrange
        var categories = CategoriesData.Categories;
        await Factory.SeedEntity(categories.ToArray());

        // Act
        var response = await Client.GetAsync("/categories");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<CategoryResponse>>();
        
        Assert.NotNull(result);
        Assert.Equal(categories.Count, result.Count);

        foreach (var category in categories)
        {
            var categoryResponse = result.SingleOrDefault(c => c.CategoryId ==category.Id);
            Assert.NotNull(categoryResponse);
            Assert.Equal(category.Name, categoryResponse.Name);
        }
    }
}