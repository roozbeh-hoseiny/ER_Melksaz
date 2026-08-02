using DQ.Abstraction.Specifications;
using DQ.Core.Expressions;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents the logical negation of a specification.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class NotSpecification<TEntity> : Specification<TEntity>
{
    private readonly ISpecification<TEntity> _specification;

    public override Expression<Func<TEntity, bool>>? Criteria
        => this._specification.Criteria?.Not();

    public NotSpecification(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        this._specification = specification;
    }

}