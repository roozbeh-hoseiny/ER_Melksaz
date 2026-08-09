using DQ.Abstraction.Projections;

namespace DQ.Core.Projections;

public abstract class Projection<TEntity, TResult> : IProjection<TEntity, TResult>
{
    public ProjectionDefinition<TEntity, TResult> Definition
    {
        get;
    }

    protected Projection(ProjectionDefinition<TEntity, TResult> definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        this.Definition = definition;
    }

}