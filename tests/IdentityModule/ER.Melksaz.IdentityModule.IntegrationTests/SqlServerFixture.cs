using Testcontainers.MsSql;

namespace ER.Melksaz.IdentityModule.IntegrationTests;

public sealed class SqlServerFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _container;

    public string ConnectionString => this._container.GetConnectionString();

    public SqlServerFixture()
    {
        this._container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Your_strong_Password123!")
            .WithCleanUp(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await this._container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await this._container.DisposeAsync();
    }
}
