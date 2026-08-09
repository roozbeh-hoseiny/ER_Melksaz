using DQ.Abstraction.Projections;
using DQ.Abstraction.Specifications;

namespace DQ.Core.Queries;

public interface IQueryDefinition<TEntity>
{
    ISpecification<TEntity> Specification { get; }
}

public interface IQueryDefinition<TEntity, TProjection> : IQueryDefinition<TEntity>
{
    IProjection<TEntity, TProjection>? Projection { get; }
}
