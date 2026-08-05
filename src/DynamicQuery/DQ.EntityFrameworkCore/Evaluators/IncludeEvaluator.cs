using DQ.Abstraction.Specifications;
using DQ.Abstraction.Specifications.Models;
using Microsoft.EntityFrameworkCore;

namespace DQ.EntityFrameworkCore.Evaluators;

public sealed class IncludeEvaluator : ISpecificationPartEvaluator
{
    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        foreach (var include in specification.Includes)
        {
            query =
                ApplyInclude(
                    query,
                    include);
        }

        return query;
    }


    private static IQueryable<TEntity> ApplyInclude<TEntity>(
        IQueryable<TEntity> query,
        IncludeDefinition<TEntity> include)
        where TEntity : class
    {
        return include switch
        {
            StringIncludeDefinition<TEntity> stringInclude
                => query.Include(
                    stringInclude.NavigationPath),


            ExpressionIncludeDefinition<TEntity, object> expressionInclude
                => query.Include(
                    expressionInclude.Expression),


            _
                => ApplyGenericExpressionInclude(
                    query,
                    include)
        };
    }


    private static IQueryable<TEntity> ApplyGenericExpressionInclude<TEntity>(
        IQueryable<TEntity> query,
        IncludeDefinition<TEntity> include)
        where TEntity : class
    {
        var type = include.GetType();


        if (!type.IsGenericType)
        {
            throw new NotSupportedException(type.FullName);
        }


        if (type.GetGenericTypeDefinition() != typeof(ExpressionIncludeDefinition<,>))
        {
            throw new NotSupportedException(type.FullName);
        }


        var expressionProperty =
            type.GetProperty(nameof(
                ExpressionIncludeDefinition<TEntity, object>.Expression));


        var expression = expressionProperty!.GetValue(include);


        var includeMethod =
            typeof(EntityFrameworkQueryableExtensions)
                .GetMethods()
                .Single(x =>
                    x.Name == nameof(EntityFrameworkQueryableExtensions.Include)
                    && x.GetParameters().Length == 2);


        var propertyType = type.GetGenericArguments()[1];


        var genericMethod =
            includeMethod.MakeGenericMethod(
                typeof(TEntity),
                propertyType);


        return (IQueryable<TEntity>)genericMethod.Invoke(
            null,
            new[]
            {
                query,
                expression
            })!;
    }
}