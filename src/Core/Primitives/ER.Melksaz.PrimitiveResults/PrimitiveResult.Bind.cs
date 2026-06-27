using ER.Melksaz.PrimitiveResults.Helpers;

namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{
    public static readonly Lazy<PrimitiveError> NullBinderResultError = LazyErrorCreator.Create("Bind.Null", "Binder returned null");

    /// <summary>
    /// Binds a successful result value to another value.
    /// </summary>
    public static PrimitiveResult<TValue> Bind<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, TValue> binder) => src.Map(binder);

    /// <summary>
    /// Binds a successful result value to another result.
    /// </summary>
    public static PrimitiveResult<TValue> Bind<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, PrimitiveResult<TValue>> binder) => src.Map(binder);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, Task<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, ValueTask<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, Task<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, TValue> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, Task<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, ValueTask<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, PrimitiveResult<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, Task<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value.
    /// </summary>
    public static PrimitiveResult<TValue> Bind<TValue>(this PrimitiveResult src, Func<TValue> binder) =>
        src.Map(binder);

    /// <summary>
    /// Binds a successful result value to another value.
    /// </summary>
    public static PrimitiveResult<TValue> Bind<TValue>(this PrimitiveResult src, Func<PrimitiveResult<TValue>> binder)
        => src.Map(binder);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this PrimitiveResult src, Func<Task<TValue>> binder) =>
        await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this PrimitiveResult src, Func<ValueTask<TValue>> binder)
        => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this PrimitiveResult src,
        Func<Task<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this PrimitiveResult src,
        Func<ValueTask<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this Task<PrimitiveResult> src,
        Func<TValue> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this Task<PrimitiveResult> src,
        Func<Task<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this Task<PrimitiveResult> src,
        Func<ValueTask<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this Task<PrimitiveResult> src,
        Func<PrimitiveResult<TValue>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this Task<PrimitiveResult> src,
        Func<Task<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);

    /// <summary>
    /// Binds a successful result value to another value asynchronously.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(
        this Task<PrimitiveResult> src,
        Func<ValueTask<PrimitiveResult<TValue>>> binder) => await src.Map(binder).ConfigureAwait(false);
}