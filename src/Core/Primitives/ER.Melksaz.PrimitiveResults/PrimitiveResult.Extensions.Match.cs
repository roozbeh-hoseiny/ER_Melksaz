using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{
    /// <summary>
    /// Transforms the specified result into an output value by executing either
    /// the success delegate or the failure delegate depending on the result state.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the successful value contained in the result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="onSuccess">
    /// The delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>,
    /// <paramref name="onSuccess"/>, or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static TOut Match<TValue, TOut>(
        PrimitiveResult<TValue> src,
        Func<TValue, TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TOut> onFailure)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return src.IsSuccess
            ? onSuccess(src.Value)
            : onFailure(src.Errors);
    }

    /// <summary>
    /// Asynchronously transforms the specified result into an output value by
    /// executing either the success delegate or the failure delegate depending
    /// on the result state.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the successful value contained in the result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The asynchronous source result.
    /// </param>
    /// <param name="onSuccess">
    /// The delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static async ValueTask<TOut> Match<TValue, TOut>(
        ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TOut> onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        PrimitiveResult<TValue> result =
            await src.ConfigureAwait(false);

        return Match(
            result,
            onSuccess,
            onFailure);
    }

    /// <summary>
    /// Asynchronously transforms the specified result into an output value by
    /// executing either the success delegate or the failure delegate depending
    /// on the result state.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the successful value contained in the result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The asynchronous source result.
    /// </param>
    /// <param name="onSuccess">
    /// The delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>,
    /// <paramref name="onSuccess"/>, or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static async ValueTask<TOut> Match<TValue, TOut>(
        Task<PrimitiveResult<TValue>> src,
        Func<TValue, TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TOut> onFailure)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        PrimitiveResult<TValue> result =
            await src.ConfigureAwait(false);

        return Match(
            result,
            onSuccess,
            onFailure);
    }

    /// <summary>
    /// Transforms the specified non-generic result into an output value by
    /// executing either the success delegate or the failure delegate depending
    /// on the result state.
    /// </summary>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="onSuccess">
    /// The delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>,
    /// <paramref name="onSuccess"/>, or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static TOut Match<TOut>(
        PrimitiveResult src,
        Func<TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TOut> onFailure)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return src.IsSuccess
            ? onSuccess()
            : onFailure(src.Errors);
    }

    /// <summary>
    /// Asynchronously transforms the specified non-generic result into an output
    /// value by executing either the success delegate or the failure delegate
    /// depending on the result state.
    /// </summary>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The asynchronous source result.
    /// </param>
    /// <param name="onSuccess">
    /// The delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static async ValueTask<TOut> Match<TOut>(
        ValueTask<PrimitiveResult> src,
        Func<TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TOut> onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        PrimitiveResult result =
            await src.ConfigureAwait(false);

        return Match(
            result,
            onSuccess,
            onFailure);
    }

    /// <summary>
    /// Asynchronously transforms the specified non-generic result into an output
    /// value by executing either the success delegate or the failure delegate
    /// depending on the result state.
    /// </summary>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The asynchronous source result.
    /// </param>
    /// <param name="onSuccess">
    /// The delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>,
    /// <paramref name="onSuccess"/>, or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static async ValueTask<TOut> Match<TOut>(
        Task<PrimitiveResult> src,
        Func<TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TOut> onFailure)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        PrimitiveResult result =
            await src.ConfigureAwait(false);

        return Match(
            result,
            onSuccess,
            onFailure);
    }

    /// <summary>
    /// Asynchronously transforms the specified result into an output value by
    /// executing either the asynchronous success delegate or the asynchronous
    /// failure delegate depending on the result state.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the successful value contained in the result.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="onSuccess">
    /// The asynchronous delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The asynchronous delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>,
    /// <paramref name="onSuccess"/>, or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static async ValueTask<TOut> MatchAsync<TValue, TOut>(
        PrimitiveResult<TValue> src,
        Func<TValue, ValueTask<TOut>> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, ValueTask<TOut>> onFailure)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return src.IsSuccess
            ? await onSuccess(src.Value).ConfigureAwait(false)
            : await onFailure(src.Errors).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously transforms the specified non-generic result into an output
    /// value by executing either the asynchronous success delegate or the
    /// asynchronous failure delegate depending on the result state.
    /// </summary>
    /// <typeparam name="TOut">
    /// The type of the output produced by the match operation.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="onSuccess">
    /// The asynchronous delegate executed when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The asynchronous delegate executed when the result is a failure.
    /// </param>
    /// <returns>
    /// The value produced by either <paramref name="onSuccess"/> or
    /// <paramref name="onFailure"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>,
    /// <paramref name="onSuccess"/>, or
    /// <paramref name="onFailure"/> is null.
    /// </exception>
    public static async ValueTask<TOut> MatchAsync<TOut>(
        PrimitiveResult src,
        Func<ValueTask<TOut>> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, ValueTask<TOut>> onFailure)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return src.IsSuccess
            ? await onSuccess().ConfigureAwait(false)
            : await onFailure(src.Errors).ConfigureAwait(false);
    }
}
