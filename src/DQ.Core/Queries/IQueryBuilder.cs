using DQ.Core.Projections;
using DQ.Core.Specifications;

namespace DQ.Core.Queries;

public interface IQueryBuilder<TEntity>
{
    ISpecificationBuilder<TEntity> Specification { get; }
    IProjectionBuilder<TEntity> Projection { get; }
    QueryDefinition<TEntity> Build();
    QueryDefinition<TEntity, TProjection> Build<TProjection>();
}
