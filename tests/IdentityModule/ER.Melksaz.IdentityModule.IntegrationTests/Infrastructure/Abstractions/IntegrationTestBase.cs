namespace ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Abstractions;

public abstract class IntegrationTestBase : IClassFixture<IntegrationTestFixture>
{
    protected HttpClient Client { get; }
    protected IServiceProvider Services { get; }

    protected IntegrationTestBase(IntegrationTestFixture fixture)
    {
        this.Client = fixture.Client;
        this.Services = fixture.Factory.Services;
    }
}