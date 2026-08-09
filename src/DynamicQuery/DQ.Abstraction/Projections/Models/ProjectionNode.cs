namespace DQ.Abstraction.Projections.Models;

public abstract record ProjectionNode;
public sealed record ProjectionPropertyNode(ProjectionMember Member) : ProjectionNode;
public sealed record ProjectionNavigationNode(ProjectionMember Member, IReadOnlyList<ProjectionNode> Children) : ProjectionNode;
public sealed record ProjectionRootNode(IReadOnlyList<ProjectionNode> Children) : ProjectionNode;
public sealed record ProjectionMember(string SourceName, string TargetName);


/*
    مثلاً یک Projection:
    Customer
    با:
    [
     "Id",
     "Name",
     "Orders.Id",
     "Orders.Amount"
    ]
    تبدیل می‌شود به:
    ProjectionRootNode
     |
     +-- ProjectionPropertyNode("Id")
     |
     +-- ProjectionPropertyNode("Name")
     |
     +-- ProjectionCollectionNode("Orders")
              |
              +-- ProjectionPropertyNode("Id")
              |
              +-- ProjectionPropertyNode("Amount")
 
 */