using DQ.Core.Projections;
using DQ.Core.Specifications;

namespace DQ.Core.Queries;

public static class QueryFactory
{
    public static IQueryBuilder<TEntity> For<TEntity>()
    {
        return new QueryBuilder<TEntity>(
            new SpecificationBuilder<TEntity>(),
            new ProjectionBuilder<TEntity>());
    }
}