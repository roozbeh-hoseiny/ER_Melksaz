using DQ.Abstraction.Projections;
using DQ.Abstraction.Projections.Models;

namespace DQ.Core.Projections;

public sealed class ProjectionBuilder<TEntity> : IProjectionBuilder<TEntity>
{
    private readonly List<ProjectionMember> _members = [];

    public IProjectionBuilder<TEntity> Include(string propertyName) => this.Include(propertyName, propertyName);
    public IProjectionBuilder<TEntity> Include(string sourceProperty, string targetProperty)
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
        var nodes = this.BuildNodes();

        var definition = new
            ProjectionDefinition<TEntity, TProjection>(
                new ProjectionRootNode(nodes));

        return new BuiltProjection<TProjection>(definition);
    }


    private IReadOnlyList<ProjectionNode> BuildNodes()
    {
        var root =
            new Dictionary<string, ProjectionNode>(
                StringComparer.OrdinalIgnoreCase);


        foreach (var member in this._members)
        {
            var segments =
                member.SourceName.Split(
                    '.',
                    StringSplitOptions.RemoveEmptyEntries);


            if (segments.Length == 0)
            {
                continue;
            }


            this.AddPath(
                root,
                segments,
                0,
                member);
        }


        return root.Values.ToList();
    }
    private void AddPath(
        IDictionary<string, ProjectionNode> nodes,
        string[] segments,
        int index,
        ProjectionMember member)
    {
        var current =
            segments[index];


        if (index == segments.Length - 1)
        {
            nodes[current] =
                new ProjectionPropertyNode(
                    member);

            return;
        }


        if (!nodes.TryGetValue(
                current,
                out var existing))
        {
            existing =
                new ProjectionNavigationNode(
                    new ProjectionMember(
                        current,
                        current),
                    []);


            nodes[current] = existing;
        }


        if (existing is not ProjectionNavigationNode navigation)
        {
            throw new InvalidOperationException(
                $"Projection path conflict on '{string.Join(".", segments)}'.");
        }


        var children =
            navigation.Children.ToDictionary(
                GetNodeName,
                StringComparer.OrdinalIgnoreCase);


        this.AddPath(
            children,
            segments,
            index + 1,
            member);


        nodes[current] =
            navigation with
            {
                Children =
                    children.Values.ToList()
            };
    }
    private static string GetNodeName(ProjectionNode node)
    {
        return node switch
        {
            ProjectionPropertyNode property =>
                GetLastSegment(
                    property.Member.SourceName),

            ProjectionNavigationNode navigation =>
                GetLastSegment(
                    navigation.Member.SourceName),

            _ =>
                throw new NotSupportedException(
                    $"Projection node type '{node.GetType().Name}' is not supported.")
        };
    }
    private static string GetLastSegment(string path)
    {
        var index =
            path.LastIndexOf('.');

        return index < 0
            ? path
            : path[(index + 1)..];
    }

    private sealed class BuiltProjection<TProjection> : Projection<TEntity, TProjection>
    {
        public BuiltProjection(
            ProjectionDefinition<TEntity, TProjection> definition)
            : base(definition) { }
    }
}