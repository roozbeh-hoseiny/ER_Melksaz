using System.Linq.Expressions;

namespace DQ.Core.Specifications.Models;

// <summary>
/// Represents the immutable state of a specification.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed record SpecificationState<TEntity>
{
    /// <summary>
    /// Gets the filtering criteria.
    /// </summary>
    public Expression<Func<TEntity, bool>>? Criteria { get; init; }


    /// <summary>
    /// Gets the navigation include definitions.
    /// </summary>
    public IReadOnlyList<IncludeDefinition<TEntity>> Includes { get; init; } = [];


    /// <summary>
    /// Gets the ordering definitions.
    /// </summary>
    public IReadOnlyList<OrderDefinition<TEntity>> Orders { get; init; } = [];


    /// <summary>
    /// Gets whether entity tracking is disabled.
    /// </summary>
    public bool AsNoTracking { get; init; }


    /// <summary>
    /// Gets whether split query execution is enabled.
    /// </summary>
    public bool AsSplitQuery { get; init; }


    /// <summary>
    /// Gets the number of records to skip.
    /// </summary>
    public int? Skip { get; init; }


    /// <summary>
    /// Gets the maximum number of records.
    /// </summary>
    public int? Take { get; init; }

}
