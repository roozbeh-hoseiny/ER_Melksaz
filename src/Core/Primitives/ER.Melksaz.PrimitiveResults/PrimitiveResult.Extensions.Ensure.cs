using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{
    #region " Sync bool predicate "

    /// <summary>
    /// Ensures that the value contained in the current result satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">
    /// A predicate that validates the contained value.
    /// </param>
    /// <param name="error">
    /// The error to return if the predicate evaluates to <c>false</c>.
    /// </param>
    /// <returns>
    /// The original result if it is already a failure or if the predicate succeeds;
    /// otherwise, a failed result containing the specified error.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>, <paramref name="predicate"/>,
    /// or <paramref name="error"/> is null.
    /// </exception>
    public static PrimitiveResult<TValue> Ensure<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        PrimitiveError error)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EnsureCore(
            src,
            (value, _) => ValueTask.FromResult(predicate(value)),
            error,
            CancellationToken.None)
            .GetAwaiter()
            .GetResult();
    }

    #endregion

    #region " Sync PrimitiveResult predicate "

    /// <summary>
    /// Ensures that the value contained in the current result satisfies
    /// a validation predicate that returns a <see cref="PrimitiveResult"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">
    /// A predicate that returns a validation result.
    /// </param>
    /// <param name="error">
    /// The error to return if the predicate result is a failure.
    /// </param>
    /// <returns>
    /// The original result if it is already a failure or if the predicate succeeds;
    /// otherwise, a failed result containing the specified error.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>, <paramref name="predicate"/>,
    /// or <paramref name="error"/> is null.
    /// </exception>
    public static PrimitiveResult<TValue> Ensure<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, PrimitiveResult> predicate,
        PrimitiveError error)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EnsureCore(
            src,
            async (value, _) =>
            {
                PrimitiveResult result = predicate(value);

                return await ValueTask.FromResult(result.IsSuccess);
            },
            error,
            CancellationToken.None)
            .GetAwaiter()
            .GetResult();
    }

    #endregion

    #region " Async bool predicate "

    /// <summary>
    /// Asynchronously ensures that the value contained in the current result
    /// satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">
    /// An asynchronous predicate that validates the contained value.
    /// </param>
    /// <param name="error">
    /// The error to return if the predicate evaluates to <c>false</c>.
    /// </param>
    /// <param name="cancellationToken">
    /// A token used to observe cancellation requests.
    /// </param>
    /// <returns>
    /// The original result if it is already a failure or if the predicate succeeds;
    /// otherwise, a failed result containing the specified error.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>, <paramref name="predicate"/>,
    /// or <paramref name="error"/> is null.
    /// </exception>
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, CancellationToken, ValueTask<bool>> predicate,
        PrimitiveError error,
        CancellationToken cancellationToken = default)
    {
        return EnsureCore(
            src,
            predicate,
            error,
            cancellationToken);
    }

    #endregion

    #region " Async PrimitiveResult predicate "

    /// <summary>
    /// Asynchronously ensures that the value contained in the current result
    /// satisfies a validation predicate that returns a <see cref="PrimitiveResult"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">
    /// An asynchronous predicate that returns a validation result.
    /// </param>
    /// <param name="error">
    /// The error to return if the predicate result is a failure.
    /// </param>
    /// <param name="cancellationToken">
    /// A token used to observe cancellation requests.
    /// </param>
    /// <returns>
    /// The original result if it is already a failure or if the predicate succeeds;
    /// otherwise, a failed result containing the specified error.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/>, <paramref name="predicate"/>,
    /// or <paramref name="error"/> is null.
    /// </exception>
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, CancellationToken, ValueTask<PrimitiveResult>> predicate,
        PrimitiveError error,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EnsureCore(
            src,
            async (value, token) =>
            {
                PrimitiveResult result =
                    await predicate(value, token)
                        .ConfigureAwait(false);

                return result.IsSuccess;
            },
            error,
            cancellationToken);
    }

    #endregion

    #region " Core "

    /// <summary>
    /// Core implementation used by all Ensure overloads.
    /// </summary>
    private static async ValueTask<PrimitiveResult<TValue>> EnsureCore<TValue>(
        PrimitiveResult<TValue> src,
        Func<TValue, CancellationToken, ValueTask<bool>> predicate,
        PrimitiveError error,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(error);

        if (src.IsFailure)
        {
            return src;
        }

        bool isValid =
            await predicate(src.Value, cancellationToken)
                .ConfigureAwait(false);

        return isValid
            ? src
            : PrimitiveResult.Failure<TValue>(error);
    }

    #endregion
}
