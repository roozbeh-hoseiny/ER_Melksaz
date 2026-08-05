using DQ.Abstraction.Specifications;
using DQ.Abstraction.Specifications.Models;
using System.Linq.Expressions;

namespace DQ.EntityFrameworkCore.Evaluators;

public sealed class OrderEvaluator : ISpecificationPartEvaluator
{
    /// <inheritdoc />
    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(specification);

        if (specification.Orders.Count == 0)
        {
            return query;
        }

        IOrderedQueryable<TEntity>? orderedQuery = null;

        foreach (var order in specification.Orders)
        {
            orderedQuery =
                ApplyOrder(
                    orderedQuery ?? query,
                    order,
                    orderedQuery is not null);
        }


        return orderedQuery!;
    }


    private static IOrderedQueryable<TEntity> ApplyOrder<TEntity>(
        IQueryable<TEntity> query,
        OrderDefinition<TEntity> order,
        bool thenBy)
        where TEntity : class
    {
        return order switch
        {
            AscendingOrderDefinition<TEntity, object> ascending
                => ApplyAscending(
                    query,
                    ascending.Expression,
                    thenBy),


            DescendingOrderDefinition<TEntity, object> descending
                => ApplyDescending(
                    query,
                    descending.Expression,
                    thenBy),


            _
                => ApplyGenericOrder(
                    query,
                    order,
                    thenBy)
        };
    }


    private static IOrderedQueryable<TEntity> ApplyAscending<TEntity>(
        IQueryable<TEntity> query,
        LambdaExpression expression,
        bool thenBy)
        where TEntity : class
    {
        var typed =
            (Expression<Func<TEntity, object>>)expression;


        return thenBy
            ? Queryable.ThenBy(
                (IOrderedQueryable<TEntity>)query,
                typed)
            : Queryable.OrderBy(
                query,
                typed);
    }


    private static IOrderedQueryable<TEntity> ApplyDescending<TEntity>(
        IQueryable<TEntity> query,
        LambdaExpression expression,
        bool thenBy)
        where TEntity : class
    {
        var typed =
            (Expression<Func<TEntity, object>>)expression;


        return thenBy
            ? Queryable.ThenByDescending(
                (IOrderedQueryable<TEntity>)query,
                typed)
            : Queryable.OrderByDescending(
                query,
                typed);
    }


    private static IOrderedQueryable<TEntity> ApplyGenericOrder<TEntity>(
        IQueryable<TEntity> query,
        OrderDefinition<TEntity> order,
        bool thenBy)
        where TEntity : class
    {
        var keyType = order.Expression.Body.Type;


        var methodName =
            order.Direction switch
            {
                OrderDirection.Ascending =>
                    thenBy
                        ? nameof(Queryable.ThenBy)
                        : nameof(Queryable.OrderBy),

                OrderDirection.Descending =>
                    thenBy
                        ? nameof(Queryable.ThenByDescending)
                        : nameof(Queryable.OrderByDescending),

                _ =>
                    throw new NotSupportedException()
            };


        var method =
            typeof(Queryable)
                .GetMethods()
                .Single(x =>
                    x.Name == methodName &&
                    x.GetParameters().Length == 2);


        var genericMethod =
            method.MakeGenericMethod(
                typeof(TEntity),
                keyType);


        return (IOrderedQueryable<TEntity>)
            genericMethod.Invoke(
                null,
                new object[]
                {
                query,
                order.Expression})!;
    }
}