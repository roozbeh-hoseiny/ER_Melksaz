using ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Factories;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Fixtures;

public sealed class SqlServerFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _container;
    private const string DatabaseName = "ER_Melksaz_Test";

    public string ConnectionString { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

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

        await this.CreateDatabaseAsync();
        this.BuildConnectionString();

        var services = TestServiceFactory.Create(this);

        await IdentityMigrator.MigrateAsync(services);

    }

    public async Task DisposeAsync()
    {
        await this._container.DisposeAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        var masterConnectionString = this._container.GetConnectionString();

        await using var connection = new SqlConnection(masterConnectionString);

        await connection.OpenAsync();

        var command = connection.CreateCommand();

        command.CommandText =
            $"IF DB_ID('{DatabaseName}') IS NULL CREATE DATABASE [{DatabaseName}]";

        await command.ExecuteNonQueryAsync();
    }

    private void BuildConnectionString()
    {
        var builder = new SqlConnectionStringBuilder(
            this._container.GetConnectionString())
        {
            InitialCatalog = DatabaseName
        };

        this.ConnectionString = builder.ConnectionString;
        this.UserId = builder.UserID;
        this.Password = builder.Password;
    }
}