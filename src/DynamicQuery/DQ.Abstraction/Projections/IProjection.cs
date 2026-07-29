using System.Linq.Expressions;

namespace DQ.Abstraction.Projections;

public interface IProjection<TEntity, TResult>
{
    Expression<Func<TEntity, TResult>> Expression { get; }
}