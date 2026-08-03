using System.Linq.Expressions;

namespace DQ.Abstraction.Specifications;

/// <summary>
/// Defines a reusable query specification for an entity.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public interface ISpecification<TEntity>
{
    /// <summary>
    /// Gets the filtering criteria.
    /// </summary>
    Expression<Func<TEntity, bool>>? Criteria { get; }

    /// <summary>
    /// Gets the navigation properties that should be included.
    /// </summary>
    IReadOnlyList<LambdaExpression> Includes { get; }

    /// <summary>
    /// Gets whether the query should disable entity tracking.
    /// </summary>
    bool AsNoTracking { get; }

    /// <summary>
    /// Gets whether the query should use split queries.
    /// </summary>
    bool AsSplitQuery { get; }

    /// <summary>
    /// Gets the number of records to skip.
    /// </summary>
    int? Skip { get; }

    /// <summary>
    /// Gets the maximum number of records to return.
    /// </summary>
    int? Take { get; }
}