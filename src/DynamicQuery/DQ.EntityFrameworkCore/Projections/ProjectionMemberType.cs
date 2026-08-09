namespace DQ.EntityFrameworkCore.Projections;

public sealed class ProjectionMemberMetadata
{
    public string Name { get; }
    public ProjectionMemberType Type { get; }
    public Type ClrType { get; }
    public ProjectionMemberMetadata(string name, ProjectionMemberType type, Type clrType)
    {
        this.Name = name;
        this.Type = type;
        this.ClrType = clrType;
    }

}
public sealed class ProjectionNavigationMetadata
{
    public string Name { get; }
    public Type ClrType { get; }
    public ProjectionNavigationKind Kind { get; }

    public ProjectionNavigationMetadata(
        string name,
        Type clrType,
        ProjectionNavigationKind kind)
    {
        this.Name = name;
        this.ClrType = clrType;
        this.Kind = kind;
    }

}
public enum ProjectionMemberType
{
    Property = 1,
    ReferenceNavigation = 2,
    CollectionNavigation = 3
}
public enum ProjectionNavigationKind
{
    Reference = 1,
    Collection = 2
}