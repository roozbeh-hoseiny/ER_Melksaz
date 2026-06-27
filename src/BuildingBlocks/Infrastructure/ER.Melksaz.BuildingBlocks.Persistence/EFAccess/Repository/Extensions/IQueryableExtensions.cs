using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;

public static class IQueryableExtensions
{
    #region " Fields "
    private static readonly ConcurrentDictionary<(Type, string, bool), LambdaExpression> _selectorCache = new();
    #endregion

    public static IOrderedQueryable<T> OrderByPropertyName<T>(this IQueryable<T> source, string propertyName, bool ascending)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = ascending ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);

        var call = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(lambda));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
    }

    public static IQueryable<T> OrderByColumns<T>(
        this IQueryable<T> source,
        IEnumerable<(string Column, bool Descending)> orderBys)
    {
        var query = source;
        bool first = true;

        foreach (var (column, descending) in orderBys)
        {
            var selector = GetOrCreateSelector<T>(column, descending);

            query = ApplyOrdering(query, selector, first, descending);
            first = false;
        }

        return query;
    }

    [return: NotNull]
    public static async Task<PrimitiveResult<TResult>> RunQueryWithError<TEntity, TResult>(
        this IQueryable<TEntity> src,
        Func<IQueryable<TEntity>, Task<TResult>> func,
        Func<PrimitiveError> nullErrorCreator)
    {
        var dbResult = await func.Invoke(src).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<TResult>(nullErrorCreator.Invoke());

        return dbResult!;
    }

    [return: NotNull]
    public static async Task<PrimitiveResult<TResult>> RunQueryWithError<TEntity, TResult>(
        this IQueryable<TEntity> src,
        Func<IQueryable<TEntity>, Task<TResult>> func,
        PrimitiveError nullError)
    {
        return await RunQueryWithError(src, func, () => nullError).ConfigureAwait(false);
    }

    [return: NotNull]
    public static async Task<PrimitiveResult<TResult>> RunQuery<TEntity, TResult>(
        this IQueryable<TEntity> src,
        Func<IQueryable<TEntity>, Task<TResult>> func,
        Func<TResult> defaultValueCreator)
    {
        var dbResult = await func.Invoke(src).ConfigureAwait(false);

        return dbResult is null
            ? PrimitiveResult.Success(defaultValueCreator.Invoke())
            : dbResult;
    }

    [return: NotNull]
    public static async Task<PrimitiveResult<TResult>> RunQuery<TEntity, TResult>(
        this IQueryable<TEntity> src,
        Func<IQueryable<TEntity>, Task<TResult>> func,
        TResult defaultValue)
    {
        return await RunQuery(src, func, () => defaultValue).ConfigureAwait(false);
    }

    #region " Private Methods "
    private static LambdaExpression GetOrCreateSelector<T>(string propertyName, bool descending)
    {
        var key = (typeof(T), propertyName, descending);

        return _selectorCache.GetOrAdd(key, k =>
        {
            var param = Expression.Parameter(typeof(T), "x");
            var body = propertyName.Split('.')
                .Aggregate<string, Expression>(param, Expression.PropertyOrField);

            // must be object to support dynamic types
            var converted = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<T, object>>(converted, param);
        });
    }

    private static IQueryable<T> ApplyOrdering<T>(
        IQueryable<T> source,
        LambdaExpression selector,
        bool first,
        bool descending)
    {
        var methodName = first
            ? (descending ? "OrderByDescending" : "OrderBy")
            : (descending ? "ThenByDescending" : "ThenBy");

        var resultExp = Expression.Call(
            typeof(Queryable),
            methodName,
            new[] { typeof(T), typeof(object) },
            source.Expression,
            Expression.Quote(selector));

        return source.Provider.CreateQuery<T>(resultExp);
    }
    #endregion
}
