using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Evaluator;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Extensions;

public static class IQueryableExtensions
{
    /// <summary>
    /// Applies a non-projecting specification to an IQueryable and then executes a custom async function.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TResult">The type of the result from the custom function.</typeparam>
    /// <param name="query">The initial IQueryable.</param>
    /// <param name="spec">The specification to apply.</param>
    /// <param name="func">The asynchronous function to execute on the specified query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the custom async function.</returns>
    public static async Task<TResult> RunQuerySpec<TEntity, TResult>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        Func<IQueryable<TEntity>, CancellationToken, Task<TResult>> func,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        // Apply the base specification (criteria, includes, ordering, paging, etc.)
        IQueryable<TEntity> specifiedQuery = SpecificationEvaluator.GetQuery(query, spec);

        return await func(specifiedQuery, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Applies a projecting specification to an IQueryable and then executes a custom async function.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TResult">The type of the result from the custom function.</typeparam>
    /// <param name="query">The initial IQueryable.</param>
    /// <param name="spec">The projecting specification to apply.</param>
    /// <param name="func">The asynchronous function to execute on the specified and projected query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the custom async function.</returns>
    public static async Task<TFinalResult> RunQuerySpec<TEntity, TQueryableResult, TFinalResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TQueryableResult> spec,
        Func<IQueryable<TQueryableResult>, CancellationToken, Task<TFinalResult>> func,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        // Apply the base specification (criteria, includes, ordering, paging, etc.)
        IQueryable<TQueryableResult> specifiedAndProjectedQuery = SpecificationEvaluator.GetQuery(query, spec);

        return await func(specifiedAndProjectedQuery, cancellationToken).ConfigureAwait(false);
    }

    #region " ToListAsync "
    public static async Task<List<TEntity>> SpecToListAsync<TEntity>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await query.RunQuerySpec(spec, async (q, ct) =>
        {
            return await q.ToListAsync(ct).ConfigureAwait(false);
        }, cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<List<TResult>> SpecToListAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await query.RunQuerySpec(spec, async (q, ct) =>
        {
            return await q.ToListAsync(ct);
        }, cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion

    #region " ToArrayAsync "
    public static async Task<TEntity[]> SpecToArrayAsync<TEntity>(
      this IQueryable<TEntity> query,
      IEFSpecification<TEntity> spec,
      CancellationToken cancellationToken = default)
      where TEntity : class
    {
        return await query.RunQuerySpec(spec, async (q, ct) =>
        {
            return await q.ToArrayAsync(ct).ConfigureAwait(false);
        }, cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<TResult[]> SpecToArrayAsync<TEntity, TResult>(
        this IQueryable<TEntity> query,
        ISpecification<TEntity, TResult> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await query.RunQuerySpec(spec, async (q, ct) =>
        {
            return await q.ToArrayAsync(ct);
        }, cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " FirstOrDefaultAsync "
    public static async Task<TEntity?> SpecFirstOrDefaultAsync<TEntity>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.FirstOrDefaultAsync(ct), cancellationToken).ConfigureAwait(false);
    }

    public static Task<TResult?> SpecFirstOrDefaultAsync<TEntity, TResult>(
       this IQueryable<TEntity> query,
       ISpecification<TEntity, TResult> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return query.RunQuerySpec(spec, (q, ct) => q.FirstOrDefaultAsync(ct), cancellationToken);
    }
    #endregion

    #region " FirstAsync "
    public static async Task<TEntity?> SpecFirstAsync<TEntity>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.FirstAsync(ct), cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<TResult> SpecFirstAsync<TEntity, TResult>(
       this IQueryable<TEntity> query,
       ISpecification<TEntity, TResult> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.FirstAsync(ct), cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " SingleOrDefaultAsync "
    public static async Task<TEntity?> SpecSingleOrDefaultAsync<TEntity>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.SingleOrDefaultAsync(ct), cancellationToken).ConfigureAwait(false);
    }

    public static async Task<TResult?> SpecSingleOrDefaultAsync<TEntity, TResult>(
       this IQueryable<TEntity> query,
       ISpecification<TEntity, TResult> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.SingleOrDefaultAsync(ct), cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " SingleAsync "
    public static async Task<TEntity?> SpecSingleAsync<TEntity>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.SingleAsync(ct), cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<TResult> SpecSingleAsync<TEntity, TResult>(
       this IQueryable<TEntity> query,
       ISpecification<TEntity, TResult> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.SingleAsync(ct), cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " CountAsync "
    public static async Task<int> SpecCountAsync<TEntity>(
        this IQueryable<TEntity> query,
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.CountAsync(ct), cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " LongCountAsync "
    public static async Task<long> SpecLongCountAsync<TEntity>(
       this IQueryable<TEntity> query,
       IEFSpecification<TEntity> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return await query.RunQuerySpec(spec, (q, ct) => q.LongCountAsync(ct), cancellationToken).ConfigureAwait(false);
    }
    #endregion
}