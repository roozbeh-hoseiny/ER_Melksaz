namespace ER.Melksaz.IdentityModule.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly SqlServerFixture _fixture;

    protected IServiceProvider Services { get; private set; } = null!;

    protected IntegrationTestBase(SqlServerFixture fixture)
    {
        this._fixture = fixture;
    }

    public Task InitializeAsync()
    {
        this.Services = TestServiceFactory.Create(this._fixture);
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        if (this.Services is IDisposable d)
            d.Dispose();

        return Task.CompletedTask;
    }
}