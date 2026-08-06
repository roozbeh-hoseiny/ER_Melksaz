using DQ.Core.Projections;
using DQ.Core.Specifications;

namespace DQ.Core.Queries;

public sealed class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
{
    private readonly ISpecificationBuilder<TEntity> _specificationBuilder;
    private readonly IProjectionBuilder<TEntity> _projectionBuilder;

    public ISpecificationBuilder<TEntity> Specification => this._specificationBuilder;
    public IProjectionBuilder<TEntity> Projection => this._projectionBuilder;

    public QueryBuilder(
        ISpecificationBuilder<TEntity> specificationBuilder,
        IProjectionBuilder<TEntity> projectionBuilder)
    {
        ArgumentNullException.ThrowIfNull(specificationBuilder);
        ArgumentNullException.ThrowIfNull(projectionBuilder);

        this._specificationBuilder = specificationBuilder;
        this._projectionBuilder = projectionBuilder;
    }

    public QueryDefinition<TEntity> Build()
    {
        return new QueryDefinition<TEntity>(this._specificationBuilder.Build());
    }
    public QueryDefinition<TEntity, TProjection> Build<TProjection>()
    {
        return new QueryDefinition<TEntity, TProjection>(
            this._specificationBuilder.Build(),
            this._projectionBuilder.Build<TProjection>());
    }
}