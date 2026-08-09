using DQ.Abstraction.Projections;

namespace DQ.EntityFrameworkCore.Projections;

public interface IProjectionEvaluator
{
    IQueryable<TResult> Apply<TEntity, TResult>(
        IQueryable<TEntity> query,
        IProjection<TEntity, TResult> projection)
        where TEntity : class;
}