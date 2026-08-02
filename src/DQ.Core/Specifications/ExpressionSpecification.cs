using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents a specification created from a LINQ expression.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class ExpressionSpecification<TEntity> : Specification<TEntity>
{
    private readonly Expression<Func<TEntity, bool>> _criteria;

    /// <inheritdoc />
    public override Expression<Func<TEntity, bool>> Criteria => this._criteria;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ExpressionSpecification{TEntity}"/> class.
    /// </summary>
    /// <param name="criteria">
    /// The predicate expression.
    /// </param>
    public ExpressionSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        this._criteria = criteria;
    }
}
