using DQ.ConsoleApp.AppCore.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DQ.ConsoleApp.AppCore;

public sealed class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(IServiceScopeFactory scopeFactory)
    {
        ArgumentNullException.ThrowIfNull(scopeFactory);

        this._scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = this._scopeFactory.CreateAsyncScope();

        var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await initializer.InitializeAsync(stoppingToken);

    }
}
