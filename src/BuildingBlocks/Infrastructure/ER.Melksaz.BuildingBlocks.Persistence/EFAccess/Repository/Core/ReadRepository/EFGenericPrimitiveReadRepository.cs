using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using ER.Melksaz.PrimitiveResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext> : IEFGenericPrimitiveReadRepository
    where TDbContext : DbContext
{
    #region " Fields "
    protected readonly TDbContext _dbContext;
    #endregion

    #region " Properties "
    protected TDbContext DbContext => this._dbContext;
    #endregion

    #region " Constructors "
    protected EFGenericPrimitiveReadRepository(TDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    #endregion

    #region " Private Methods "
    private PrimitiveResult<IQueryable<TResult>> GenerateFilter<TEntity, TResult>(
        Expression<Func<TEntity, bool>>? predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        int maxCount = -1)
        where TEntity : class
    {
        if (predicate is null)
        {
            predicate = (_) => true;
        }

        var q = this._dbContext.Set<TEntity>()
                        .Where(predicate);

        return (sort, projection, maxCount) switch
        {
            (not null, not null, <= 0) => PrimitiveResult.Success(
                        q.OrderBy(sort!)
                        .Select(projection)),

            (not null, not null, > 0) => PrimitiveResult.Success(
                q.OrderBy(sort!).Take(maxCount)
                .Select(projection)),

            (not null, null, <= 0) => PrimitiveResult.Success(
                        q.OrderBy(sort!)
                        .Cast<TResult>()),

            (not null, null, > 0) => PrimitiveResult.Success(
                q.OrderBy(sort!).Take(maxCount)
                .Cast<TResult>()),

            (null, not null, <= 0) => this.GetPrimaryKeys<TEntity>()
                .Map(primaryKeys => OrderByColumns(q, primaryKeys))
                .Map(a => a.Select(projection)),

            (null, not null, > 0) => this.GetPrimaryKeys<TEntity>()
                .Map(primaryKeys => OrderByColumns(q, primaryKeys).Take(maxCount))
                .Map(a => a.Select(projection)),

            (null, null, <= 0) => this.GetPrimaryKeys<TEntity>()
                .Map(primaryKeys => OrderByColumns(q, primaryKeys))
                .Map(a => a.Cast<TResult>()),

            (null, null, > 0) => this.GetPrimaryKeys<TEntity>()
                .Map(primaryKeys => OrderByColumns(q, primaryKeys).Take(maxCount))
                .Map(a => a.Cast<TResult>())
        };
    }
    private PrimitiveResult<string[]> GetPrimaryKeys<TEntity>() where TEntity : class
    {
        var pks = this._dbContext.GetPrimaryKeys<TEntity>();
        if (pks.Length == 0)
            return PrimitiveResult.Failure<string[]>(EFRepositoryErrors.Generate_Entity_PrimaryKey_Not_Found_Error<string[]>());

        return PrimitiveResult.Success(pks);
    }
    private static IQueryable<T> OrderByColumns<T>(IQueryable<T> source, string[] columns)
    {
        if (columns == null || columns.Length == 0)
            return source;

        var type = typeof(T);
        var parameter = Expression.Parameter(type, "x");

        IQueryable<T> result = source;
        bool firstOrder = true;

        foreach (var column in columns)
        {
            var property = type.GetProperty(column);
            if (property == null)
                throw new ArgumentException($"Property '{column}' does not exist on type '{type.Name}'");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = firstOrder ? "OrderBy" : "ThenBy";
            var orderByCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { type, property.PropertyType },
                result.Expression,
                Expression.Quote(orderByExp));

            result = result.Provider.CreateQuery<T>(orderByCall);
            firstOrder = false;
        }

        return result;
    }
    private Expression<Func<TEntity, bool>> GenerateEntityPrimaryKeyEqualityExpression<TEntity>(
        string[] primaryKeys, object id) where TEntity : class
    {
        // Create parameter expression
        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
        // Create equality expressions for primary key property
        var equalityExpression = Expression.Equal(Expression.Property(parameter, primaryKeys.First()), Expression.Constant(id));
        // Create lambda expression
        Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameter);

        return lambda;
    }
    #endregion
}