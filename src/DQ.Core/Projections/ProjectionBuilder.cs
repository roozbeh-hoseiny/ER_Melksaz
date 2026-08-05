namespace DQ.Core.Projections;

/*
    هدف:
    
    ورودی:
    [
        "Id",
        "Name",
        "Orders.Id",
        "Orders.Amount"
    ]
    
    خروجی:
    ProjectionRootNode
     |
     +-- Id
     +-- Name
     +-- Orders
           |
           +-- Id
           +-- Amount

    مثال استفاده:

    var definition =
        new ProjectionBuilder<Customer>()
            .Include("Id")
            .Include("Name", "DisplayName")
            .Include("Orders.Id")
            .Include("Orders.Amount")
            .Build<CustomerDto>();


    var expression =
        projectionExpressionBuilder
            .Build(definition);


    var query =
        db.Customers
          .Select(expression);

    خروجی Expression:

    x => new CustomerDto
    {
        Id = x.Id,

        DisplayName = x.Name,

        Orders =
            x.Orders
              .Select(item => new OrderDto
              {
                  Id = item.Id,
                  Amount = item.Amount
              })
              .ToList()
    }
    
 */
public sealed class ProjectionBuilder<TEntity>
{
    private readonly List<ProjectionMember> _members = [];

    public ProjectionBuilder<TEntity> Include(string propertyName) => this.Include(propertyName, propertyName);


    public ProjectionBuilder<TEntity> Include(string sourceProperty, string targetProperty)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceProperty);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetProperty);

        this._members.Add(
            new ProjectionMember(
                sourceProperty,
                targetProperty));

        return this;
    }

    public ProjectionDefinition<TEntity, TProjection> Build<TProjection>()
    {
        var root =
            new ProjectionRootNode(
                this.BuildNodes());

        return new ProjectionDefinition<TEntity, TProjection>(
            root);
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
                    Array.Empty<ProjectionNode>());

            nodes[current] = existing;
        }


        if (existing is not ProjectionNavigationNode collection)
        {
            throw new InvalidOperationException(
                $"Projection path conflict on '{current}'.");
        }


        var children =
            collection.Children
                .ToDictionary(
                    x => x switch
                    {
                        ProjectionPropertyNode p
                            => p.Member.SourceName,

                        ProjectionNavigationNode c
                            => c.Member.SourceName,

                        _ => string.Empty
                    },
                    StringComparer.OrdinalIgnoreCase);


        this.AddPath(
            children,
            segments,
            index + 1,
            member);


        nodes[current] =
            collection with
            {
                Children =
                    children.Values.ToList()
            };
    }
}