using DQ.Abstraction.Specifications;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents the base implementation of a strongly typed specification.
///
/// <para>
/// A specification encapsulates a reusable business rule expressed as a LINQ
/// predicate. Specifications can be combined using logical operators to
/// construct more complex queries without duplicating filtering logic.
/// </para>
///
/// <para>
/// This class is immutable. Every logical composition creates a new
/// specification instance and never modifies existing ones.
/// </para>
/// </summary>
/// <typeparam name="TEntity">
/// The entity type to which the specification applies.
/// </typeparam>
public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    /// <summary>
    /// Gets the filtering criteria represented by this specification.
    /// </summary>
    public abstract Expression<Func<TEntity, bool>>? Criteria { get; }

    /// <summary>
    /// Combines the current specification with another specification using
    /// a logical AND operation.
    /// </summary>
    /// <param name="specification">
    /// The specification to combine with the current instance.
    /// </param>
    /// <returns>
    /// A new specification representing the logical AND of both specifications.
    /// </returns>
    public Specification<TEntity> And(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return new AndSpecification<TEntity>(this, specification);
    }

    /// <summary>
    /// Combines the current specification with another specification using
    /// a logical OR operation.
    /// </summary>
    /// <param name="specification">
    /// The specification to combine with the current instance.
    /// </param>
    /// <returns>
    /// A new specification representing the logical OR of both specifications.
    /// </returns>
    public Specification<TEntity> Or(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return new OrSpecification<TEntity>(this, specification);
    }

    /// <summary>
    /// Negates the current specification.
    /// </summary>
    /// <returns>
    /// A new specification representing the logical NOT of the current specification.
    /// </returns>
    public Specification<TEntity> Not()
    {
        return new NotSpecification<TEntity>(this);
    }
}