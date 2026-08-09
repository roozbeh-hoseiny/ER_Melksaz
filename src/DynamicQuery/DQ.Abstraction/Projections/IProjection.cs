using DQ.Core.Projections;

namespace DQ.Abstraction.Projections;

public interface IProjection<TEntity, TResult>
{
    ProjectionDefinition<TEntity, TResult> Definition { get; }
}