using ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Containers;
using ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Factories;

namespace ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Fixtures;

public sealed class IntegrationTestFixture : IAsyncLifetime
{
    public SqlServerContainer SqlContainer { get; }
    public CustomWebApplicationFactory Factory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;

    public IntegrationTestFixture()
    {
        this.SqlContainer = new SqlServerContainer();
    }
    public async Task InitializeAsync()
    {
        await this.SqlContainer.InitializeAsync();

        this.Factory = new CustomWebApplicationFactory(this.SqlContainer);

        await this.Factory.InitializeAsync();

        this.Client = this.Factory.CreateClient();

    }
    public async Task DisposeAsync()
    {
        this.Client.Dispose();
        await this.Factory.DisposeAsync();
        await this.SqlContainer.DisposeAsync();
    }
}