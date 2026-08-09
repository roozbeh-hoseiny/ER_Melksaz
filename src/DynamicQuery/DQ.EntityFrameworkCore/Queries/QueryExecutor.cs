using DQ.Core.Queries;
using DQ.EntityFrameworkCore.Projections;
using DQ.EntityFrameworkCore.Specifications;

namespace DQ.EntityFrameworkCore.Queries;

public sealed class QueryExecutor : IQueryExecutor
{
    private readonly ISpecificationEvaluator _specificationEvaluator;
    private readonly IProjectionEvaluator _projectionEvaluator;

    public QueryExecutor(
        ISpecificationEvaluator specificationEvaluator,
        IProjectionEvaluator projectionEvaluator)
    {
        ArgumentNullException.ThrowIfNull(specificationEvaluator);
        ArgumentNullException.ThrowIfNull(projectionEvaluator);

        this._specificationEvaluator = specificationEvaluator;
        this._projectionEvaluator = projectionEvaluator;
    }

    public IQueryable<TEntity> Execute<TEntity>(
        IQueryable<TEntity> query,
        QueryDefinition<TEntity> definition)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(definition);

        return this._specificationEvaluator.Apply(
            query,
            definition.Specification);
    }

    public IQueryable<TProjection> Execute<TEntity, TProjection>(
        IQueryable<TEntity> query,
        QueryDefinition<TEntity, TProjection> definition)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(definition.Projection);

        var specificationQuery = this._specificationEvaluator.Apply(
                query,
                definition.Specification);

        return this._projectionEvaluator.Apply(
            specificationQuery,
            definition.Projection);
    }
}
