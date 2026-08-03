using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents the internal immutable state of a specification.
/// </summary>
/// <remarks>
/// The state object separates specification data from specification behavior.
/// Every modification creates a new state instance.
/// </remarks>
internal sealed record SpecificationState
{
    /// <summary>
    /// Gets the filtering criteria.
    /// </summary>
    public LambdaExpression? Criteria { get; init; }


    /// <summary>
    /// Gets the navigation properties to include.
    /// </summary>
    public IReadOnlyList<LambdaExpression> Includes { get; init; } = Array.Empty<LambdaExpression>();


    /// <summary>
    /// Gets whether tracking is disabled.
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


    /// <summary>
    /// Gets the ordering definitions.
    /// </summary>
    public IReadOnlyList<OrderDefinition> Orders { get; init; } = Array.Empty<OrderDefinition>();
}
