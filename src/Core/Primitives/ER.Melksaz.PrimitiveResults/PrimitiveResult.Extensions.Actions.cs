namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{
    #region " DoSync "

    /// <summary>
    /// Executes the specified action regardless of the result state
    /// and returns the original result.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="act">
    /// The action to execute.
    /// </param>
    /// <returns>
    /// The original <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/> or <paramref name="act"/> is null.
    /// </exception>
    public static PrimitiveResult<TValue> Do<TValue>(
        this PrimitiveResult<TValue> src,
        Action<PrimitiveResult<TValue>> act)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(act);

        act(src);

        return src;
    }

    #endregion

    #region " OnSuccessSync "

    /// <summary>
    /// Executes the specified action only when the result is successful
    /// and returns the original result.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="act">
    /// The action to execute when the result is successful.
    /// </param>
    /// <returns>
    /// The original <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/> or <paramref name="act"/> is null.
    /// </exception>
    public static PrimitiveResult<TValue> OnSuccess<TValue>(
        this PrimitiveResult<TValue> src,
        Action<PrimitiveResult<TValue>> act)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(act);

        if (src.IsSuccess)
            act(src);

        return src;
    }

    #endregion

    #region " OnFailureSync "

    /// <summary>
    /// Executes the specified action only when the result represents a failure
    /// and returns the original result.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <param name="src">
    /// The source result.
    /// </param>
    /// <param name="act">
    /// The action to execute when the result is a failure.
    /// </param>
    /// <returns>
    /// The original <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/> or <paramref name="act"/> is null.
    /// </exception>
    public static PrimitiveResult<TValue> OnFailure<TValue>(
        this PrimitiveResult<TValue> src,
        Action<PrimitiveResult<TValue>> act)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(act);

        if (src.IsFailure)
            act(src);

        return src;
    }

    #endregion

    #region " DoAsync "

    /// <summary>
    /// Asynchronously executes the specified action regardless of the result state
    /// and returns the original result.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <param name="src">
    /// The source asynchronous result.
    /// </param>
    /// <param name="act">
    /// The asynchronous action to execute.
    /// </param>
    /// <returns>
    /// A task containing the original <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TValue>> Do<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<PrimitiveResult<TValue>, ValueTask> act)
    {
        var result = await src.ConfigureAwait(false);
        await act(result).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region " OnSuccessAsync "

    /// <summary>
    /// Asynchronously executes the specified action only when the result is successful
    /// and returns the original result.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<PrimitiveResult<TValue>, ValueTask> act)
    {
        var result = await src.ConfigureAwait(false);

        if (result.IsSuccess)
            await act(result).ConfigureAwait(false);

        return result;
    }

    #endregion

    #region " OnFailureAsync "

    /// <summary>
    /// Asynchronously executes the specified action only when the result represents a failure
    /// and returns the original result.
    /// </summary>
    public static async ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<PrimitiveResult<TValue>, ValueTask> act)
    {
        var result = await src.ConfigureAwait(false);

        if (result.IsFailure)
            await act(result).ConfigureAwait(false);

        return result;
    }

    #endregion


}
