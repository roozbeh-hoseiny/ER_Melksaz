using DQ.Abstraction.Projections;
using DQ.Abstraction.Specifications;
using DQ.Core.Projections;
using DQ.Core.Specifications;

namespace DQ.Core.Queries;

public sealed class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
{
    private readonly ISpecificationBuilder<TEntity> _specificationBuilder;
    private readonly IProjectionBuilder<TEntity> _projectionBuilder;

    private ISpecification<TEntity>? _specification;

    private object? _projection;

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

    public IQueryBuilder<TEntity> WithSpecification(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        this._specification = specification;

        return this;
    }

    public IQueryBuilder<TEntity> WithProjection<TProjection>(IProjection<TEntity, TProjection> projection)
    {
        ArgumentNullException.ThrowIfNull(projection);

        this._projection = projection;

        return this;
    }

    public QueryDefinition<TEntity> Build()
    {
        var specification =
            this._specification
            ?? this._specificationBuilder.Build();

        return new QueryDefinition<TEntity>(specification);
    }

    public QueryDefinition<TEntity, TProjection> Build<TProjection>()
    {
        var specification =
            this._specification
            ?? this._specificationBuilder.Build();

        var projection =
            this._projection as IProjection<TEntity, TProjection>
            ?? this._projectionBuilder.Build<TProjection>();

        return new QueryDefinition<TEntity, TProjection>(
            specification,
            projection);
    }
}