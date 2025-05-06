using Xunit;

namespace Demo.Common.Tests.Functional;

public class BaseControllerTests : IClassFixture<FunctionalDatabaseFixture>
{
    protected readonly HttpClient _client;

    public BaseControllerTests(FunctionalDatabaseFixture fixture)
    {
        var factory = new CustomWebApplicationFactory(fixture.ConnectionString);
        _client = factory.CreateClient();
    }
}