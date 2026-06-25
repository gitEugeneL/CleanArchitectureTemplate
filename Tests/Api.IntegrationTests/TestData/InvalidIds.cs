using System.Collections;

namespace Api.IntegrationTests.TestData;

public class InvalidIds : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [Guid.CreateVersion7()];
        yield return [Guid.CreateVersion7()];
        yield return [Guid.CreateVersion7()];
        yield return [Guid.CreateVersion7()];
        yield return [Guid.NewGuid()];
        yield return [Guid.NewGuid()];
        yield return [Guid.NewGuid()];
        yield return [Guid.NewGuid()];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}