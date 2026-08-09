namespace DQ.Core.Queries;

public interface IQueryExecutor
{
    IQueryable<TEntity> Execute<TEntity>(
        IQueryable<TEntity> query,
        QueryDefinition<TEntity> definition)
        where TEntity : class;

    IQueryable<TProjection> Execute<TEntity, TProjection>(
        IQueryable<TEntity> query,
        QueryDefinition<TEntity, TProjection> definition) where TEntity : class;

}