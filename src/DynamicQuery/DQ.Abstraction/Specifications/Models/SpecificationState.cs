namespace DQ.Abstraction.Specifications.Models;

public sealed record SpecificationState<TEntity>
{
    public CriteriaNode<TEntity>? Criteria { get; init; }
    public IReadOnlyList<IncludeDefinition<TEntity>> Includes { get; init; } = [];
    public IReadOnlyList<OrderDefinition<TEntity>> Orders { get; init; } = [];
    public bool AsNoTracking { get; init; }
    public bool AsNoTrackingWithIdentityResolution { get; init; }
    public bool AsSplitQuery { get; init; }
    public int? Skip { get; init; }
    public int? Take { get; init; }
}