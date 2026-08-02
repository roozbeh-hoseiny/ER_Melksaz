using DQ.Abstraction.Specifications;
using DQ.Core.Expressions;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents the logical AND of two specifications.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class AndSpecification<TEntity> : CompositeSpecification<TEntity>
{
    public override Expression<Func<TEntity, bool>>? Criteria
    {
        get
        {
            if (this.Left.Criteria is null)
                return this.Right.Criteria;

            if (this.Right.Criteria is null)
                return this.Left.Criteria;

            return this.Left.Criteria.And(this.Right.Criteria);
        }
    }

    public AndSpecification(
        ISpecification<TEntity> left,
        ISpecification<TEntity> right)
        : base(left, right)
    {
    }

}
