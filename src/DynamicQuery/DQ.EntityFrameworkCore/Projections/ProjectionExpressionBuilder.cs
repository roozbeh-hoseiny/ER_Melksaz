using DQ.Abstraction.Projections.Models;
using DQ.Core.Projections;
using System.Linq.Expressions;

namespace DQ.EntityFrameworkCore.Projections;

/*

 مثال

    Projection:

    builder
        .Include("Id")
        .Include("Name")
        .Include("Address.City")
        .Include("Orders.Id")
        .Include("Orders.Amount")
        .Build<CustomerDto>();

    Expression تولید شده:

    entity =>
    new CustomerDto
    {
        Id = entity.Id,

        Name = entity.Name,

        Address =
            new AddressDto
            {
                City = entity.Address.City
            },

        Orders =
            entity.Orders
                .Select(item =>
                    new OrderDto
                    {
                        Id = item.Id,
                        Amount = item.Amount
                    })
                .ToList()
    }
 */
public sealed class ProjectionExpressionBuilder
{
    private readonly ProjectionMetadataResolver _metadataResolver;

    public ProjectionExpressionBuilder(ProjectionMetadataResolver metadataResolver)
    {
        ArgumentNullException.ThrowIfNull(metadataResolver);

        this._metadataResolver = metadataResolver;
    }


    public Expression<Func<TEntity, TProjection>> Build<TEntity, TProjection>(
        ProjectionDefinition<TEntity, TProjection> definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        var parameter =
            Expression.Parameter(
                typeof(TEntity),
                "entity");

        var body =
            this.BuildObject(
                typeof(TProjection),
                typeof(TEntity),
                parameter,
                definition.Root.Children);


        return Expression.Lambda<Func<TEntity, TProjection>>(
            body,
            parameter);
    }


    private Expression BuildObject(
        Type destinationType,
        Type sourceType,
        Expression source,
        IReadOnlyList<ProjectionNode> nodes)
    {
        var bindings =
            new List<MemberBinding>();


        foreach (var node in nodes)
        {
            switch (node)
            {
                case ProjectionPropertyNode propertyNode:

                    this.AddPropertyBinding(
                        destinationType,
                        source,
                        propertyNode,
                        bindings);

                    break;


                case ProjectionNavigationNode navigationNode:

                    this.AddNavigationBinding(
                        destinationType,
                        sourceType,
                        source,
                        navigationNode,
                        bindings);

                    break;


                default:

                    throw new NotSupportedException(
                        $"Projection node '{node.GetType().Name}' is not supported.");
            }
        }


        return Expression.MemberInit(
            Expression.New(destinationType),
            bindings);
    }


    private void AddPropertyBinding(
        Type destinationType,
        Expression source,
        ProjectionPropertyNode node,
        List<MemberBinding> bindings)
    {
        var destinationProperty =
            destinationType.GetProperty(
                node.Member.TargetName);


        if (destinationProperty is null)
        {
            return;
        }


        var sourceProperty =
            Expression.Property(
                source,
                node.Member.SourceName);


        bindings.Add(
            Expression.Bind(
                destinationProperty,
                sourceProperty));
    }


    private void AddNavigationBinding(
        Type destinationType,
        Type sourceType,
        Expression source,
        ProjectionNavigationNode node,
        List<MemberBinding> bindings)
    {
        var destinationProperty =
            destinationType.GetProperty(
                node.Member.TargetName);


        if (destinationProperty is null)
        {
            return;
        }


        var metadata =
            this._metadataResolver.ResolveNavigation(
                sourceType,
                node.Member.SourceName);


        var navigationExpression =
            Expression.Property(
                source,
                node.Member.SourceName);


        Expression projection;


        switch (metadata.Kind)
        {
            case ProjectionNavigationKind.Reference:

                projection =
                    this.BuildObject(
                        destinationProperty.PropertyType,
                        metadata.ClrType,
                        navigationExpression,
                        node.Children);

                break;


            case ProjectionNavigationKind.Collection:

                projection =
                    this.BuildCollection(
                        destinationProperty.PropertyType,
                        metadata.ClrType,
                        navigationExpression,
                        node.Children);

                break;


            default:

                throw new NotSupportedException(
                    $"Navigation kind '{metadata.Kind}' is not supported.");
        }


        bindings.Add(
            Expression.Bind(
                destinationProperty,
                projection));
    }


    private Expression BuildCollection(
        Type destinationCollectionType,
        Type sourceElementType,
        Expression source,
        IReadOnlyList<ProjectionNode> children)
    {
        var destinationElementType =
            GetCollectionElementType(
                destinationCollectionType);


        var parameter =
            Expression.Parameter(
                sourceElementType,
                "item");


        var body =
            this.BuildObject(
                destinationElementType,
                sourceElementType,
                parameter,
                children);


        var selector =
            Expression.Lambda(
                body,
                parameter);


        var select =
            Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Select),
                new[]
                {
                    sourceElementType,
                    destinationElementType
                },
                source,
                selector);


        return Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.ToList),
            new[]
            {
                destinationElementType
            },
            select);
    }


    private static Type GetCollectionElementType(
        Type collectionType)
    {
        if (collectionType.IsArray)
        {
            return collectionType.GetElementType()!;
        }


        var enumerable =
            collectionType
                .GetInterfaces()
                .FirstOrDefault(
                    x =>
                        x.IsGenericType &&
                        x.GetGenericTypeDefinition() ==
                        typeof(IEnumerable<>));


        if (enumerable is null)
        {
            throw new InvalidOperationException(
                $"Type '{collectionType.Name}' is not a collection.");
        }


        return enumerable.GetGenericArguments()[0];
    }
}