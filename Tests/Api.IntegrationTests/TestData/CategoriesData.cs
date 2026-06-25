using System.Collections;
using Domain.Entities.Categories;

namespace Api.IntegrationTests.TestData;

public class CategoriesData : IEnumerable<object[]>
{
    public static readonly List<Category> Categories =
    [
        new("Electronics"),
        new("Sport"),
        new("Home"),
        new("Cars"),
        new("Toys")
    ];
    
    public IEnumerator<object[]> GetEnumerator()
    {
        return Categories.Select(c => (object[])[c]).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}