using Microsoft.EntityFrameworkCore.Metadata;

namespace DQ.EntityFrameworkCore.Projections;

/*
    هدف :
    
    قبل از ساخت Expression بفهمیم هر مسیر چه نوعی است:

    مثلاً:

    Customer
     |
     +-- Id              => Scalar Property
     |
     +-- Address         => Reference Navigation
     |       |
     |       +-- City
     |
     +-- Orders           => Collection Navigation
             |
             +-- Amount

    برای این کار از Metadata خود EF Core استفاده می‌کنیم، نه Reflection.
 */
public sealed class ProjectionMetadataResolver
{
    private readonly IModel _model;

    public ProjectionMetadataResolver(IModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        this._model = model;
    }


    public ProjectionMemberMetadata Resolve(Type entityType, string memberName)
    {
        var entity = this._model.FindEntityType(entityType);

        if (entity is null)
        {
            throw new InvalidOperationException($"Entity '{entityType.Name}' was not found.");
        }

        var property = entity.FindProperty(memberName);

        if (property is not null)
        {
            return new ProjectionMemberMetadata(
                memberName,
                ProjectionMemberType.Property,
                property.ClrType);
        }


        var navigation = entity.FindNavigation(memberName);

        if (navigation is null)
        {
            throw new InvalidOperationException($"Member '{memberName}' was not found on '{entityType.Name}'.");
        }


        if (navigation.IsCollection)
        {
            return new ProjectionMemberMetadata(
                memberName,
                ProjectionMemberType.CollectionNavigation,
                navigation.TargetEntityType.ClrType);
        }


        return new ProjectionMemberMetadata(
            memberName,
            ProjectionMemberType.ReferenceNavigation,
            navigation.TargetEntityType.ClrType);
    }

    public ProjectionNavigationMetadata ResolveNavigation(Type entityType, string navigationName)
    {
        var entity =
            this._model.FindEntityType(
                entityType);


        if (entity is null)
        {
            throw new InvalidOperationException(
                $"Entity '{entityType.Name}' not found.");
        }


        var navigation =
            entity.FindNavigation(
                navigationName);


        if (navigation is null)
        {
            throw new InvalidOperationException(
                $"Navigation '{navigationName}' not found.");
        }


        return new ProjectionNavigationMetadata(
            navigationName,
            navigation.TargetEntityType.ClrType,
            navigation.IsCollection
                ? ProjectionNavigationKind.Collection
                : ProjectionNavigationKind.Reference);
    }
}
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