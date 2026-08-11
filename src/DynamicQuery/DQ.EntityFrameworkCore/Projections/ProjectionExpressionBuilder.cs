using DQ.Abstraction.Projections.Models;
using System.Linq.Expressions;
using System.Reflection;

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
public sealed class ProjectionExpressionBuilder<TEntity, TResult>
{
    public Expression<Func<TEntity, TResult>> Build(
        IReadOnlyList<ProjectionMember> members)
    {
        ArgumentNullException.ThrowIfNull(members);

        var parameter =
            Expression.Parameter(
                typeof(TEntity),
                "entity");

        var body =
            this.BuildObject(
                parameter,
                typeof(TEntity),
                typeof(TResult),
                members,
                string.Empty);

        return Expression.Lambda<Func<TEntity, TResult>>(
            body,
            parameter);
    }

    private Expression BuildObject(
        Expression source,
        Type sourceType,
        Type targetType,
        IReadOnlyList<ProjectionMember> members,
        string path)
    {
        var bindings =
            new List<MemberBinding>();

        foreach (var targetProperty in
                 targetType.GetProperties(
                     BindingFlags.Instance |
                     BindingFlags.Public))
        {
            if (!targetProperty.CanWrite)
            {
                continue;
            }

            var explicitMember =
                this.FindExplicitMember(
                    members,
                    path,
                    targetProperty.Name);

            Expression? value;

            if (explicitMember is not null)
            {
                value =
                    this.BuildExplicitValue(
                        source,
                        explicitMember,
                        targetProperty,
                        members);
            }
            else
            {
                value =
                    this.BuildConventionValue(
                        source,
                        targetProperty,
                        members,
                        path);
            }

            if (value is null)
            {
                continue;
            }

            bindings.Add(
                Expression.Bind(
                    targetProperty,
                    value));
        }

        return Expression.MemberInit(
            Expression.New(targetType),
            bindings);
    }

    private Expression? BuildConventionValue(
        Expression source,
        PropertyInfo targetProperty,
        IReadOnlyList<ProjectionMember> members,
        string path)
    {
        var sourceProperty =
            source.Type.GetProperty(
                targetProperty.Name,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.IgnoreCase);

        if (sourceProperty is null)
        {
            return null;
        }

        var sourceValue =
            Expression.Property(
                source,
                sourceProperty);

        return this.BuildValue(
            sourceValue,
            sourceProperty.PropertyType,
            targetProperty.PropertyType,
            members,
            this.CombinePath(
                path,
                sourceProperty.Name));
    }

    private Expression? BuildExplicitValue(
        Expression source,
        ProjectionMember member,
        PropertyInfo targetProperty,
        IReadOnlyList<ProjectionMember> members)
    {
        var sourceValue =
            this.ResolvePath(
                source,
                member.SourceName);

        if (sourceValue is null)
        {
            throw new InvalidOperationException(
                $"Projection source '{member.SourceName}' " +
                $"could not be resolved from '{source.Type.Name}'.");
        }

        return this.BuildValue(
            sourceValue,
            sourceValue.Type,
            targetProperty.PropertyType,
            members,
            member.SourceName);
    }

    private Expression? BuildValue(
        Expression source,
        Type sourceType,
        Type targetType,
        IReadOnlyList<ProjectionMember> members,
        string path)
    {
        if (this.CanAssign(
                sourceType,
                targetType))
        {
            return this.ConvertIfRequired(
                source,
                targetType);
        }

        if (this.IsCollection(
                sourceType,
                targetType,
                out var sourceElementType,
                out var targetElementType))
        {
            return this.BuildCollection(
                source,
                sourceElementType,
                targetElementType,
                targetType,
                members,
                path);
        }

        if (this.CanBuildComplexType(
                sourceType,
                targetType))
        {
            return this.BuildObject(
                source,
                sourceType,
                targetType,
                members,
                path);
        }

        return null;
    }

    private Expression BuildCollection(
        Expression source,
        Type sourceElementType,
        Type targetElementType,
        Type targetCollectionType,
        IReadOnlyList<ProjectionMember> members,
        string path)
    {
        var parameter =
            Expression.Parameter(
                sourceElementType,
                "item");

        var body =
            this.BuildObject(
                parameter,
                sourceElementType,
                targetElementType,
                members,
                path);

        var selector =
            Expression.Lambda(
                body,
                parameter);

        var select =
            Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Select),
                [
                    sourceElementType,
                    targetElementType
                ],
                source,
                selector);

        if (targetCollectionType.IsArray)
        {
            return Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.ToArray),
                [targetElementType],
                select);
        }

        return Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.ToList),
            [targetElementType],
            select);
    }

    private Expression? ResolvePath(
        Expression source,
        string path)
    {
        var current =
            source;

        foreach (var segment in path.Split(
                     '.',
                     StringSplitOptions.RemoveEmptyEntries))
        {
            var property =
                current.Type.GetProperty(
                    segment,
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.IgnoreCase);

            if (property is null)
            {
                return null;
            }

            current =
                Expression.Property(
                    current,
                    property);
        }

        return current;
    }

    private ProjectionMember? FindExplicitMember(
        IReadOnlyList<ProjectionMember> members,
        string currentPath,
        string targetProperty)
    {
        var targetPath =
            this.CombinePath(
                currentPath,
                targetProperty);

        return members.FirstOrDefault(
            x =>
                string.Equals(
                    x.TargetName,
                    targetPath,
                    StringComparison.OrdinalIgnoreCase)
                ||
                (
                    string.IsNullOrEmpty(currentPath) &&
                    string.Equals(
                        x.TargetName,
                        targetProperty,
                        StringComparison.OrdinalIgnoreCase)
                ));
    }

    private bool IsCollection(
        Type sourceType,
        Type targetType,
        out Type sourceElementType,
        out Type targetElementType)
    {
        sourceElementType = null!;
        targetElementType = null!;

        if (sourceType == typeof(string) ||
            targetType == typeof(string))
        {
            return false;
        }

        sourceElementType =
            GetElementType(sourceType)!;

        targetElementType =
            GetElementType(targetType)!;

        return sourceElementType is not null &&
               targetElementType is not null;
    }

    private static Type? GetElementType(Type type)
    {
        if (type.IsArray)
        {
            return type.GetElementType();
        }

        if (type.IsGenericType &&
            type.GetGenericTypeDefinition() ==
            typeof(IEnumerable<>))
        {
            return type.GetGenericArguments()[0];
        }

        return type.GetInterfaces()
            .Where(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() ==
                typeof(IEnumerable<>))
            .Select(x =>
                x.GetGenericArguments()[0])
            .FirstOrDefault();
    }

    private bool CanBuildComplexType(
        Type sourceType,
        Type targetType)
    {
        if (sourceType == typeof(string) ||
            targetType == typeof(string))
        {
            return false;
        }

        if (sourceType.IsValueType ||
            targetType.IsValueType)
        {
            return false;
        }

        if (targetType.IsInterface ||
            targetType.IsAbstract)
        {
            return false;
        }

        return targetType.GetConstructor(
            Type.EmptyTypes) is not null;
    }

    private bool CanAssign(
        Type sourceType,
        Type targetType)
    {
        if (targetType.IsAssignableFrom(sourceType))
        {
            return true;
        }

        var targetUnderlying =
            Nullable.GetUnderlyingType(
                targetType);

        if (targetUnderlying is not null)
        {
            return targetUnderlying.IsAssignableFrom(
                sourceType);
        }

        var sourceUnderlying =
            Nullable.GetUnderlyingType(
                sourceType);

        if (sourceUnderlying is not null)
        {
            return targetType.IsAssignableFrom(
                sourceUnderlying);
        }

        return false;
    }

    private Expression ConvertIfRequired(
        Expression expression,
        Type targetType)
    {
        if (expression.Type == targetType)
        {
            return expression;
        }

        return Expression.Convert(
            expression,
            targetType);
    }

    private string CombinePath(
        string parent,
        string child)
    {
        return string.IsNullOrEmpty(parent)
            ? child
            : $"{parent}.{child}";
    }
}