using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " RunSpecification "
    public async Task<PrimitiveResult<TResult>> RunSpecification<TEntity, TResult>(
        IEFSpecification<TEntity> spec,
        Func<IQueryable<TEntity>, CancellationToken, Task<TResult>> func,
        Func<TResult> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .RunQuerySpec(spec, func, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TResult>> RunSpecification<TEntity, TResult>(
        IEFSpecification<TEntity> spec,
        Func<IQueryable<TEntity>, CancellationToken, Task<TResult>> func,
        TResult defaulValue,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunSpecification(
            spec,
            func,
            () => defaulValue,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TResult>> RunSpecification<TEntity, TResult>(
        IEFSpecification<TEntity> spec,
        Func<IQueryable<TEntity>, CancellationToken, Task<TResult>> func,
        Func<PrimitiveError> errorCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .RunQuerySpec(spec, func, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return PrimitiveResult.Failure<TResult>(errorCreator.Invoke());

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TResult>> RunSpecification<TEntity, TResult>(
        IEFSpecification<TEntity> spec,
        Func<IQueryable<TEntity>, CancellationToken, Task<TResult>> func,
        PrimitiveError error,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunSpecification(
            spec,
            func,
            () => error,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunSpecification<TEntity, TQueryableResult, TFinalResult>(
        ISpecification<TEntity, TQueryableResult> spec,
        Func<IQueryable<TQueryableResult>, CancellationToken, Task<TFinalResult>> func,
        Func<TFinalResult> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .RunQuerySpec(spec, func, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunSpecification<TEntity, TQueryableResult, TFinalResult>(
        ISpecification<TEntity, TQueryableResult> spec,
        Func<IQueryable<TQueryableResult>, CancellationToken, Task<TFinalResult>> func,
        TFinalResult defaulValue,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunSpecification(
            spec,
            func,
            () => defaulValue,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunSpecification<TEntity, TQueryableResult, TFinalResult>(
        ISpecification<TEntity, TQueryableResult> spec,
        Func<IQueryable<TQueryableResult>, CancellationToken, Task<TFinalResult>> func,
        Func<PrimitiveError> errorCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .RunQuerySpec(spec, func, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return PrimitiveResult.Failure<TFinalResult>(errorCreator.Invoke());

        return PrimitiveResult.Success(specResult!);
    }
    public async Task<PrimitiveResult<TFinalResult>> RunSpecification<TEntity, TQueryableResult, TFinalResult>(
        ISpecification<TEntity, TQueryableResult> spec,
        Func<IQueryable<TQueryableResult>, CancellationToken, Task<TFinalResult>> func,
        PrimitiveError error,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunSpecification(
            spec,
            func,
            () => error,
            cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion

    #region " RunToListSpecification "
    public async Task<PrimitiveResult<List<TEntity>>> RunToListSpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<List<TEntity>> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecToListAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<List<TFinalResult>>> RunToListSpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        Func<List<TFinalResult>> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecToListAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<List<TEntity>>> RunToListSpecification<TEntity>(
       IEFSpecification<TEntity> spec,
       List<TEntity> defaulValue,
       CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToListSpecification(
            spec,
            () => defaulValue,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<List<TFinalResult>>> RunToListSpecification<TEntity, TFinalResult>(
      ISpecification<TEntity, TFinalResult> spec,
      List<TFinalResult> defaulValue,
      CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToListSpecification(
            spec,
            () => defaulValue,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<List<TEntity>>> RunToListSpecification<TEntity>(
       IEFSpecification<TEntity> spec,
       CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToListSpecification(
            spec,
            () => Array.Empty<TEntity>().ToList(),
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<List<TFinalResult>>> RunToListSpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToListSpecification(
            spec,
            () => Array.Empty<TFinalResult>().ToList(),
            cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion

    #region " RunToArraySpecification "
    public async Task<PrimitiveResult<TEntity[]>> RunToArraySpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<TEntity[]> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecToArrayAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TEntity[]>> RunToArraySpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        TEntity[] defaulValue,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToArraySpecification(
            spec,
            () => defaulValue,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TEntity[]>> RunToArraySpecification<TEntity>(
       IEFSpecification<TEntity> spec,
       CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToArraySpecification(
            spec,
            () => Array.Empty<TEntity>(),
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TFinalResult[]>> RunToArraySpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        Func<TFinalResult[]> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecToArrayAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TFinalResult[]>> RunToArraySpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        TFinalResult[] defaulValue,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToArraySpecification(
            spec,
            () => defaulValue,
            cancellationToken)
            .ConfigureAwait(false);
    }
    public async Task<PrimitiveResult<TFinalResult[]>> RunToArraySpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunToArraySpecification(
            spec,
            () => Array.Empty<TFinalResult>(),
            cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion

    #region " RunFirstOrDefaultSpecification "
    public async Task<PrimitiveResult<TEntity>> RunFirstOrDefaultSpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<TEntity> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecFirstOrDefaultAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TEntity>> RunFirstOrDefaultSpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        TEntity defaultValue,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunFirstOrDefaultSpecification(spec, () => defaultValue, cancellationToken).ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunFirstOrDefaultSpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        Func<TFinalResult> defaulValueCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecFirstOrDefaultAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return defaulValueCreator.Invoke();

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunFirstOrDefaultSpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        TFinalResult defaultValue,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunFirstOrDefaultSpecification(spec, () => defaultValue, cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " RunFirstOrErrorSpecification "
    public async Task<PrimitiveResult<TEntity>> RunFirstOrErrorSpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<PrimitiveError> defaulErrorCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecFirstOrDefaultAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return PrimitiveResult.Failure<TEntity>(defaulErrorCreator.Invoke());

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TEntity>> RunFirstOrErrorSpecification<TEntity>(
        IEFSpecification<TEntity> spec,
        PrimitiveError defaultError,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunFirstOrErrorSpecification(spec, () => defaultError, cancellationToken).ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunFirstOrErrorSpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        Func<PrimitiveError> defaulErrorCreator,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var specResult = await this._dbContext.Set<TEntity>()
            .SpecFirstOrDefaultAsync(spec, cancellationToken)
            .ConfigureAwait(false);

        if (specResult is null) return PrimitiveResult.Failure<TFinalResult>(defaulErrorCreator.Invoke());

        return PrimitiveResult.Success(specResult!);
    }

    public async Task<PrimitiveResult<TFinalResult>> RunFirstOrErrorSpecification<TEntity, TFinalResult>(
        ISpecification<TEntity, TFinalResult> spec,
        PrimitiveError defaultError,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return await this.RunFirstOrErrorSpecification(spec, () => defaultError, cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region " RunSpecCountAsync "
    public async Task<PrimitiveResult<int>> RunSpecCountAsync<TEntity>(
       IEFSpecification<TEntity> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return await this._dbContext.Set<TEntity>()
            .SpecCountAsync(spec, cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion

    #region " RunSpecLongCountAsync "
    public async Task<PrimitiveResult<long>> RunSpecLongCountAsync<TEntity>(
       IEFSpecification<TEntity> spec,
       CancellationToken cancellationToken = default)
       where TEntity : class
    {
        return await this._dbContext.Set<TEntity>()
            .SpecLongCountAsync(spec, cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion
}
