using DQ.Abstraction.Projections;
using DQ.Abstraction.Projections.Models;

namespace DQ.Core.Projections;

public sealed class ProjectionBuilder<TEntity> : IProjectionBuilder<TEntity>
{
    private readonly List<ProjectionMember> _members = [];

    public IProjectionBuilder<TEntity> Include(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return this.Include(
            propertyName,
            propertyName);
    }

    public IProjectionBuilder<TEntity> Include(
        string sourceProperty,
        string targetProperty)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceProperty);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetProperty);

        this._members.Add(
            new ProjectionMember(
                sourceProperty,
                targetProperty));

        return this;
    }

    public IProjection<TEntity, TProjection> Build<TProjection>()
    {
        var definition =
            new ProjectionDefinition<TEntity, TProjection>(
                this._members);

        return new BuiltProjection<TProjection>(
            definition);
    }

    public IProjectionBuilder<TEntity> Include(params IEnumerable<ProjectionMember> members)
    {
        foreach (var member in members)
            this._members.Add(member);
        return this;
    }

    private sealed class BuiltProjection<TProjection>
        : Projection<TEntity, TProjection>
    {
        public BuiltProjection(
            ProjectionDefinition<TEntity, TProjection> definition)
            : base(definition)
        {
        }
    }
}