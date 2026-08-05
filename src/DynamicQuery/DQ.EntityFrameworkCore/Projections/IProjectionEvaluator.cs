using DQ.Abstraction.Specifications;
using DQ.Core.Projections;

namespace DQ.EntityFrameworkCore.Projections;

public interface IProjectionEvaluator
{
    IQueryable<TProjection> Apply<TEntity, TProjection>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class;
}
public sealed class ProjectionEvaluator
    : IProjectionEvaluator
{
    private readonly ProjectionExpressionBuilder _builder;

    public ProjectionEvaluator(ProjectionExpressionBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        this._builder = builder;
    }

    public IQueryable<TProjection> Apply<TEntity, TProjection>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(specification);


        var projection = specification.Projection;


        if (projection is null)
        {
            throw new InvalidOperationException(
                "Projection is not defined.");
        }


        var expression =
            this._builder.Build<TEntity, TProjection>(
                (ProjectionDefinition<TEntity, TProjection>)projection);


        return query.Select(expression);
    }
}