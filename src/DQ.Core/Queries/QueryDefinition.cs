using DQ.Abstraction.Projections;
using DQ.Abstraction.Specifications;

namespace DQ.Core.Queries;

public sealed class QueryDefinition<TEntity> : IQueryDefinition<TEntity>
{
    public ISpecification<TEntity> Specification { get; }

    public QueryDefinition(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        this.Specification = specification;
    }
}
public sealed class QueryDefinition<TEntity, TProjection> : IQueryDefinition<TEntity, TProjection>
{
    public ISpecification<TEntity> Specification { get; }
    public IProjection<TEntity, TProjection>? Projection { get; }

    public QueryDefinition(ISpecification<TEntity> specification, IProjection<TEntity, TProjection>? projection)
    {
        ArgumentNullException.ThrowIfNull(specification);

        this.Specification = specification;
        this.Projection = projection;
    }
}
