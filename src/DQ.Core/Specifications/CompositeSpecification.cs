using DQ.Abstraction.Specifications;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents the base type for specifications composed from one or more
/// child specifications.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public abstract class CompositeSpecification<TEntity> : Specification<TEntity>
{
    /// <summary>
    /// Gets the left specification.
    /// </summary>
    protected ISpecification<TEntity> Left { get; }

    /// <summary>
    /// Gets the right specification.
    /// </summary>
    protected ISpecification<TEntity> Right { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeSpecification{TEntity}"/> class.
    /// </summary>
    /// <param name="left">
    /// The left specification.
    /// </param>
    /// <param name="right">
    /// The right specification.
    /// </param>
    protected CompositeSpecification(
        ISpecification<TEntity> left,
        ISpecification<TEntity> right)
    {
        this.Left = left;
        this.Right = right;
    }


}
