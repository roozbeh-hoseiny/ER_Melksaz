using ER.Melksaz.PrimitiveResults;
using ER.Melksaz.PrimitiveResults.Helpers;

namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{
    public static readonly Lazy<PrimitiveError> NullMapperResultError =
        LazyErrorCreator.Create("Map.Null", "Mapper returned null");

    #region " ===================== CORE ===================== "

    /// <summary>
    /// Core implementation for Map operations.
    /// </summary>
    private static PrimitiveResult<TOut> MapCore<TValue, TOut>(
        PrimitiveResult<TValue> src,
        Func<TValue, TOut> mapper)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(mapper);

        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var result = mapper(src.Value);

        if (result is null && default(TOut) is null)
            return PrimitiveResult.Failure<TOut>(NullMapperResultError.Value);

        return PrimitiveResult.Success(result);
    }
    #endregion

    #region " ===================== MAP ===================== "

    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static PrimitiveResult<TOut> Map<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, TOut> mapper) => MapCore(src, mapper);

    /// <summary>
    /// Maps a successful result value to another result.
    /// </summary>
    public static PrimitiveResult<TOut> Map<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, PrimitiveResult<TOut>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        return mapper(src.Value);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, Task<TOut>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        var v = await mapper(src.Value).ConfigureAwait(false);
        return MapCore(src, _ => v);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, ValueTask<TOut>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        var v = await mapper(src.Value).ConfigureAwait(false);
        return MapCore(src, _ => v);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, Task<PrimitiveResult<TOut>>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        return await mapper(src.Value).ConfigureAwait(false);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        return await mapper(src.Value).ConfigureAwait(false);
    }

    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, TOut> mapper) => MapCore(await src.ConfigureAwait(false), mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, Task<TOut>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, ValueTask<TOut>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, PrimitiveResult<TOut>> mapper) => (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, Task<PrimitiveResult<TOut>>> mapper)
        => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper)
        => await (await src.ConfigureAwait(false)).Map(mapper);
    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, TOut> mapper) => MapCore(await src.ConfigureAwait(false), mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, Task<TOut>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, ValueTask<TOut>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, PrimitiveResult<TOut>> mapper) => (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, Task<PrimitiveResult<TOut>>> mapper)
        => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper)
        => await (await src.ConfigureAwait(false)).Map(mapper);
    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static PrimitiveResult<TOut> Map<TOut>(this PrimitiveResult src, Func<TOut> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        var v = mapper();
        if (v is null && default(TOut) is null)
            return PrimitiveResult.Failure<TOut>("Map.Null", "Mapper returned null");
        return PrimitiveResult.Success(v);
    }

    /// <summary>
    /// Maps a successful result value to another value.
    /// </summary>
    public static PrimitiveResult<TOut> Map<TOut>(this PrimitiveResult src, Func<PrimitiveResult<TOut>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        return mapper();
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(this PrimitiveResult src, Func<Task<TOut>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        var v = await mapper().ConfigureAwait(false);
        return PrimitiveResult.Success(v);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(this PrimitiveResult src, Func<ValueTask<TOut>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        var v = await mapper().ConfigureAwait(false);
        return PrimitiveResult.Success(v);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this PrimitiveResult src,
        Func<Task<PrimitiveResult<TOut>>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        return await mapper().ConfigureAwait(false);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this PrimitiveResult src,
        Func<ValueTask<PrimitiveResult<TOut>>> mapper)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TOut>(src.Errors);
        ArgumentNullException.ThrowIfNull(mapper);
        return await mapper().ConfigureAwait(false);
    }

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this Task<PrimitiveResult> src,
        Func<TOut> mapper) => (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this Task<PrimitiveResult> src,
        Func<Task<TOut>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this Task<PrimitiveResult> src,
        Func<ValueTask<TOut>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this Task<PrimitiveResult> src,
        Func<PrimitiveResult<TOut>> mapper) => (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this Task<PrimitiveResult> src,
        Func<Task<PrimitiveResult<TOut>>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    /// <summary>
    /// Maps a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TOut>(
        this Task<PrimitiveResult> src,
        Func<ValueTask<PrimitiveResult<TOut>>> mapper) => await (await src.ConfigureAwait(false)).Map(mapper);

    #endregion
}