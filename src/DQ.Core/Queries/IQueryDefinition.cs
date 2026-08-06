using DQ.Abstraction.Specifications;
using DQ.Core.Projections;

namespace DQ.Core.Queries;

public interface IQueryDefinition<TEntity>
{
    ISpecification<TEntity> Specification { get; }
}

public interface IQueryDefinition<TEntity, TProjection> : IQueryDefinition<TEntity>
{
    ProjectionDefinition<TEntity, TProjection>? Projection { get; }
}
