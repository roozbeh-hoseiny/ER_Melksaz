using DQ.Abstraction.Specifications;

namespace DQ.Core.Queries;

public interface IQueryExecutor
{
    IQueryable<TEntity> Execute<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class;

    IQueryable<TProjection> Execute<TEntity, TProjection>(
        IQueryable<TEntity> query,
        QueryDefinition<TEntity, TProjection> definition)
        where TEntity : class;
}