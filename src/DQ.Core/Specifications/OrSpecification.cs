using DQ.Abstraction.Specifications;
using DQ.Core.Expressions;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents the logical OR of two specifications.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class OrSpecification<TEntity> : CompositeSpecification<TEntity>
{
    public override Expression<Func<TEntity, bool>>? Criteria
    {
        get
        {
            if (this.Left.Criteria is null)
                return this.Right.Criteria;

            if (this.Right.Criteria is null)
                return this.Left.Criteria;

            return this.Left.Criteria.Or(this.Right.Criteria);
        }
    }
    public OrSpecification(
        ISpecification<TEntity> left,
        ISpecification<TEntity> right)
        : base(left, right)
    {
    }

}
