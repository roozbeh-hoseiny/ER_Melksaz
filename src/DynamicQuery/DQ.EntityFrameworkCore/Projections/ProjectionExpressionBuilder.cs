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
    public Expression<Func<TEntity, TResult>> Build(IReadOnlyList<ProjectionMember> members)
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
                members);

        return Expression.Lambda<Func<TEntity, TResult>>(
            body,
            parameter);
    }

    private Expression BuildObject(
    Expression source,
    Type sourceType,
    Type targetType,
    IReadOnlyList<ProjectionMember> members)
    {
        var bindings = new List<MemberBinding>();

        var isAutoProjection =
            members.Count == 0;

        foreach (var targetProperty in targetType.GetProperties(
                     BindingFlags.Instance |
                     BindingFlags.Public))
        {
            if (!targetProperty.CanWrite)
            {
                continue;
            }

            Expression? value;

            if (isAutoProjection)
            {
                value = this.BuildConventionValue(
                    source,
                    targetProperty,
                    members);
            }
            else
            {
                value = this.BuildExplicitValue(
                    source,
                    sourceType,
                    targetProperty,
                    members);
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

    private Expression? BuildExplicitValue(
    Expression source,
    Type sourceType,
    PropertyInfo targetProperty,
    IReadOnlyList<ProjectionMember> members)
    {
        var propertyName =
            targetProperty.Name;

        // Direct:
        //
        // CreatedAt
        //
        var directMember =
            members.FirstOrDefault(x =>
                string.Equals(
                    x.TargetName,
                    propertyName,
                    StringComparison.OrdinalIgnoreCase));

        if (directMember is not null)
        {
            var sourceExpression =
                this.ResolvePath(
                    source,
                    directMember.SourceName);

            if (sourceExpression is null)
            {
                throw new InvalidOperationException(
                    $"Source property '{directMember.SourceName}' " +
                    $"could not be resolved from '{sourceType.Name}'.");
            }

            return this.BuildValue(
                sourceExpression,
                sourceExpression.Type,
                targetProperty.PropertyType,
                members);
        }

        // Nested:
        //
        // Orders.CreatedAt
        //
        var prefix =
            propertyName + ".";

        var nestedMembers =
            members
                .Where(x =>
                    x.TargetName.StartsWith(
                        prefix,
                        StringComparison.OrdinalIgnoreCase))
                .Select(x =>
                    new ProjectionMember(
                        RemovePrefix(
                            x.SourceName,
                            prefix),
                        RemovePrefix(
                            x.TargetName,
                            prefix)))
                .ToList();

        if (nestedMembers.Count == 0)
        {
            return null;
        }

        var sourceProperty =
            sourceType.GetProperty(
                propertyName,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.IgnoreCase);

        if (sourceProperty is null)
        {
            throw new InvalidOperationException(
                $"Source property '{propertyName}' " +
                $"could not be resolved from '{sourceType.Name}'.");
        }

        var sourceExpressionForNested =
            Expression.Property(
                source,
                sourceProperty);

        return this.BuildNestedValue(
            sourceExpressionForNested,
            targetProperty.PropertyType,
            nestedMembers);
    }

    private Expression? BuildConventionValue(
    Expression source,
    PropertyInfo targetProperty,
    IReadOnlyList<ProjectionMember> members)
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
            members);
    }

    private Expression BuildNestedValue(
    Expression source,
    Type targetType,
    IReadOnlyList<ProjectionMember> members)
    {
        if (this.IsCollection(
                source.Type,
                targetType,
                out var sourceElementType,
                out var targetElementType))
        {
            return this.BuildCollection(
                source,
                sourceElementType,
                targetElementType,
                targetType,
                members);
        }

        return this.BuildObject(
            source,
            source.Type,
            targetType,
            members);
    }

    private Expression? BuildValue(
    Expression source,
    Type sourceType,
    Type targetType,
    IReadOnlyList<ProjectionMember> members)
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
                members);
        }

        if (members.Count > 0 &&
            this.CanBuildComplexType(
                sourceType,
                targetType))
        {
            return this.BuildObject(
                source,
                sourceType,
                targetType,
                members);
        }

        return null;
    }

    private Expression BuildCollection(
    Expression source,
    Type sourceElementType,
    Type targetElementType,
    Type targetCollectionType,
    IReadOnlyList<ProjectionMember> members)
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
                members);

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

    private static string RemovePrefix(
    string value,
    string prefix)
    {
        return value.StartsWith(
            prefix,
            StringComparison.OrdinalIgnoreCase)
            ? value[prefix.Length..]
            : value;
    }

    private Expression? ResolvePath(
        Expression source,
        string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return source;
        }

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

        var sourceElement =
            GetElementType(sourceType);

        var targetElement =
            GetElementType(targetType);

        if (sourceElement is null ||
            targetElement is null)
        {
            return false;
        }

        sourceElementType = sourceElement;
        targetElementType = targetElement;

        return true;
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

}