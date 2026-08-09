using DQ.Abstraction.Projections;

namespace DQ.EntityFrameworkCore.Projections;

public sealed class ProjectionEvaluator : IProjectionEvaluator
{
    private readonly ProjectionExpressionBuilder _builder;

    public ProjectionEvaluator(ProjectionExpressionBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        this._builder = builder;
    }

    public IQueryable<TProjection> Apply<TEntity, TProjection>(
        IQueryable<TEntity> query,
        IProjection<TEntity, TProjection> projection)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(projection);

        var expression = this._builder.Build(projection.Definition);

        return query.Select(expression);
    }
}