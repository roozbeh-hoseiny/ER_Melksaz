using System.Linq.Expressions;

namespace DQ.Core.Specifications.Buidlers;

/// <summary>
/// Provides a fluent API for creating specifications.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class SpecificationBuilder<TEntity>
{
    private Specification<TEntity> _specification;

    internal SpecificationBuilder()
    {
        this._specification = new EmptySpecification<TEntity>();
    }


    /// <summary>
    /// Adds a filtering criteria using logical AND.
    /// </summary>
    /// <param name="criteria">
    /// The filtering expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> criteria)
    {
        var specification = new ExpressionSpecification<TEntity>(criteria);

        this._specification = this._specification.And(specification);

        return this;
    }


    /// <summary>
    /// Adds a filtering criteria using logical AND.
    /// </summary>
    /// <param name="criteria">
    /// The filtering expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> And(Expression<Func<TEntity, bool>> criteria)
    {
        return this.Where(criteria);
    }


    /// <summary>
    /// Adds a filtering criteria using logical OR.
    /// </summary>
    /// <param name="criteria">
    /// The filtering expression.
    /// </param>
    /// <returns>
    /// The current builder instance.
    /// </returns>
    public SpecificationBuilder<TEntity> Or(Expression<Func<TEntity, bool>> criteria)
    {
        var specification = new ExpressionSpecification<TEntity>(criteria);

        this._specification = this._specification.Or(specification);

        return this;
    }


    /// <summary>
    /// Builds the final specification.
    /// </summary>
    /// <returns>
    /// A composed specification instance.
    /// </returns>
    public Specification<TEntity> Build()
    {
        return this._specification;
    }
}
