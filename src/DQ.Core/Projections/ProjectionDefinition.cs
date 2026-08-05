namespace DQ.Core.Projections;

public sealed class ProjectionDefinition<TEntity, TProjection>
{
    public ProjectionRootNode Root
    {
        get;
    }

    public ProjectionDefinition(ProjectionRootNode root)
    {
        ArgumentNullException.ThrowIfNull(root);

        this.Root = root;
    }


}