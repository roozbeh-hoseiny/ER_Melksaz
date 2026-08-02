using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents an empty specification with no filtering criteria.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed class EmptySpecification<TEntity> : Specification<TEntity>
{
    /// <inheritdoc />
    public override Expression<Func<TEntity, bool>>? Criteria => null;
}