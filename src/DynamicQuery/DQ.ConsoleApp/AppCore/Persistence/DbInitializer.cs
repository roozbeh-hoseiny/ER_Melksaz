using DQ.ConsoleApp.AppCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DQ.ConsoleApp.AppCore.Persistence;

public sealed class DbInitializer
{
    private readonly AppDbContext _dbContext;

    public DbInitializer(AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        this._dbContext = dbContext;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await this._dbContext.Database.EnsureCreatedAsync(cancellationToken);

        if (await this._dbContext.Customers.AnyAsync(cancellationToken))
        {
            return;
        }

        var customers =
            new[]
            {
                new Customer
                {
                    Name = "Alice",
                    IsActive = true
                },
                new Customer
                {
                    Name = "Bob",
                    IsActive = true
                },
                new Customer
                {
                    Name = "Charlie",
                    IsActive = false
                },
                new Customer
                {
                    Name = "David",
                    IsActive = true
                },
                new Customer
                {
                    Name = "Eva",
                    IsActive = false
                }
            };

        await this._dbContext.Customers.AddRangeAsync(customers, cancellationToken);

        await this._dbContext.SaveChangesAsync(cancellationToken);

        var orders =
            new[]
            {
                new Order
                {
                    CustomerId = customers[0].Id,
                    Amount = 120,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Order
                {
                    CustomerId = customers[0].Id,
                    Amount = 250,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Order
                {
                    CustomerId = customers[1].Id,
                    Amount = 80,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Order
                {
                    CustomerId = customers[3].Id,
                    Amount = 500,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };

        await this._dbContext.Orders.AddRangeAsync(
            orders,
            cancellationToken);

        await this._dbContext.SaveChangesAsync(
            cancellationToken);
    }
}