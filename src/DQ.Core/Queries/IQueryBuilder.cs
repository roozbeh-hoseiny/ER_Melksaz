using DQ.Abstraction.Projections;
using DQ.Abstraction.Specifications;
using DQ.Core.Projections;
using DQ.Core.Specifications;

namespace DQ.Core.Queries;

public interface IQueryBuilder<TEntity>
{
    ISpecificationBuilder<TEntity> Specification { get; }
    IProjectionBuilder<TEntity> Projection { get; }

    IQueryBuilder<TEntity> WithSpecification(ISpecification<TEntity> specification);

    IQueryBuilder<TEntity> WithProjection<TProjection>(IProjection<TEntity, TProjection> projection);

    QueryDefinition<TEntity> Build();
    QueryDefinition<TEntity, TProjection> Build<TProjection>();
}
