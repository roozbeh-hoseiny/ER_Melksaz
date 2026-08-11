using DQ.Abstraction.Projections;

namespace DQ.EntityFrameworkCore.Projections;

public sealed class ProjectionEvaluator : IProjectionEvaluator
{
    public IQueryable<TResult> Apply<TEntity, TResult>(
         IQueryable<TEntity> query,
         IProjection<TEntity, TResult> projection)
         where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(projection);

        var expression =
            new ProjectionExpressionBuilder<TEntity, TResult>()
                .Build(projection.Definition.Members);

        return query.Select(expression);
    }
}