using DQ.Abstraction.Projections.Models;

namespace DQ.Core.Projections;

public sealed class ProjectionDefinition<TEntity, TResult>
{
    public IReadOnlyList<ProjectionMember> Members { get; }

    public ProjectionDefinition(IReadOnlyList<ProjectionMember> members)
    {
        ArgumentNullException.ThrowIfNull(members);

        this.Members = members;
    }
}