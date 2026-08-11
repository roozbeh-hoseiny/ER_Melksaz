using DQ.Abstraction.Specifications;
using DQ.ConsoleApp.AppCore.DTOs;
using DQ.ConsoleApp.AppCore.Entities;
using DQ.ConsoleApp.AppCore.Persistence;
using DQ.Core.Projections;
using DQ.Core.Queries;
using DQ.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DQ.ConsoleApp.AppCore;

public sealed class QueryTestService
{
    private readonly AppDbContext _dbContext;
    private readonly IQueryExecutor _queryExecutor;

    public QueryTestService(
        AppDbContext dbContext,
        IQueryExecutor queryExecutor)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(queryExecutor);

        this._dbContext = dbContext;
        this._queryExecutor = queryExecutor;
    }

    public async Task RunAsync(
        CancellationToken cancellationToken = default)
    {
        await this.TestWhereAsync(cancellationToken);

        await this.TestAndAsync(cancellationToken);

        await this.TestOrAsync(cancellationToken);

        await this.TestIncludeAsync(cancellationToken);

        await this.TestStringIncludeAsync(cancellationToken);

        await this.TestOrderByAsync(cancellationToken);

        await this.TestPagingAsync(cancellationToken);

        await this.TestAsNoTrackingAsync(cancellationToken);

        await this.TestAsNoTrackingWithIdentityResolutionAsync(
            cancellationToken);

        await this.TestAsTrackingAsync(cancellationToken);

        await this.TestAsSplitQueryAsync(cancellationToken);

        await this.TestProjectionAsync(cancellationToken);

        await this.TestSpecificationAndProjectionAsync(
            cancellationToken);
    }

    private async Task TestWhereAsync(CancellationToken cancellationToken)
    {
        var queryBuilder = QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Where(x => x.IsActive);

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Where: {customers.Count}");
    }

    private async Task TestAndAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Where(x => x.IsActive)
            .And(x => x.Name.Contains("a"));

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"And: {customers.Count}");
    }

    private async Task TestOrAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Where(x => x.Name == "Alice")
            .Or(x => x.Name == "Bob");

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Or: {customers.Count}");
    }

    private async Task TestIncludeAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Include(x => x.Orders);

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Include(Expression): {customers.Count}");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"  {customer.Name}: " +
                $"{customer.Orders.Count} orders");
        }
    }

    private async Task TestStringIncludeAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Include("Orders");

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Include(String): {customers.Count}");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"  {customer.Name}: " +
                $"{customer.Orders.Count} orders");
        }
    }

    private async Task TestOrderByAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .OrderBy(x => x.Name);

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine("OrderBy:");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"  {customer.Name}");
        }
    }

    private async Task TestPagingAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .OrderBy(x => x.Id)
            .Skip(1)
            .Take(2);

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Paging: {customers.Count}");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"  {customer.Id} - {customer.Name}");
        }
    }

    private async Task TestAsNoTrackingAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .AsNoTracking();

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"AsNoTracking: {customers.Count}");
    }

    private async Task TestAsNoTrackingWithIdentityResolutionAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Include(x => x.Orders)
            .AsNoTrackingWithIdentityResolution();

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"AsNoTrackingWithIdentityResolution: " +
            $"{customers.Count}");
    }

    private async Task TestAsTrackingAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .AsTracking();

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"AsTracking: {customers.Count}");
    }

    private async Task TestAsSplitQueryAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Include(x => x.Orders)
            .AsSplitQuery();

        var definition =
            queryBuilder.Build();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"AsSplitQuery: {customers.Count}");
    }

    private async Task TestProjectionAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name);

        queryBuilder.Projection
            .Include(nameof(Customer.Id))
            .Include(nameof(Customer.Name))
            .Include(nameof(Customer.IsActive));

        var definition =
            queryBuilder
                .Build<CustomerListDto>();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Projection: {customers.Count}");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"  {customer.Id} - " +
                $"{customer.Name} - " +
                $"{customer.IsActive}");
        }
    }

    private async Task TestSpecificationAndProjectionAsync(
        CancellationToken cancellationToken)
    {
        var queryBuilder =
            QueryFactory.For<Customer>();

        queryBuilder.Specification
            .Where(x => x.IsActive)
            .Include(x => x.Orders)
            .OrderBy(x => x.Name)
            .AsNoTracking();

        queryBuilder.Projection
            .Include(nameof(Customer.Id))
            .Include(nameof(Customer.Name))
            .Include(nameof(Customer.IsActive));

        var definition =
            queryBuilder
                .Build<CustomerListDto>();

        var query =
            this._queryExecutor.Execute(
                this._dbContext.Customers,
                definition);

        var customers =
            await query.ToListAsync(
                cancellationToken);

        Console.WriteLine(
            $"Specification + Projection: " +
            $"{customers.Count}");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"  {customer.Id} - {customer.Name}");
        }
    }
}

public sealed class ActiveCustomersSpecification : Specification<Customer>
{
    public ActiveCustomersSpecification() : base(builder =>
    {
        builder
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name);
    })
    { }

    public ISpecification<Customer> Build(string? search = null)
    {
        var builder =
            QueryFactory
                .For<Customer>()
                .Specification;

        builder.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(search))
        {
            builder.And(x => x.Name.Contains(search));
        }

        builder.OrderBy(x => x.Name);

        return builder.Build();
    }
}
public sealed class CustomerListDtoProjection : Projection<Customer, CustomerListDto>
{
    public CustomerListDtoProjection() : base(builder =>
        builder
        .Include(nameof(CustomerListDto.Id))
        .Include(nameof(CustomerListDto.IsActive))
        .Include(nameof(CustomerListDto.Name)))
    {
    }
}