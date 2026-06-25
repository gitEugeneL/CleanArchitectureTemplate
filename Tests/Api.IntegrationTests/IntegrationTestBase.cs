namespace Api.IntegrationTests;

[Collection(nameof(IntegrationTestCollection))]
public class IntegrationTestBase(ApplicationFactory factory) : IAsyncLifetime
{
    protected readonly HttpClient Client = factory.CreateClient();
    protected readonly ApplicationFactory Factory = factory;
    
    public async Task InitializeAsync()
    {
        await Factory.ResetDatabaseAsync();
        // await Factory.ResetCacheAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}