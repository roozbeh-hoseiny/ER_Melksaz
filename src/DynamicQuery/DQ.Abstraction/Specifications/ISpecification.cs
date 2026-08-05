using DQ.Abstraction.Specifications.Models;

namespace DQ.Abstraction.Specifications;

public interface ISpecification<TEntity>
{
    CriteriaNode<TEntity>? Criteria { get; }
    IReadOnlyList<IncludeDefinition<TEntity>> Includes { get; }
    IReadOnlyList<OrderDefinition<TEntity>> Orders { get; }
    bool AsNoTracking { get; }
    bool AsNoTrackingWithIdentityResolution { get; }
    bool AsSplitQuery { get; }
    int? Skip { get; }
    int? Take { get; }
}