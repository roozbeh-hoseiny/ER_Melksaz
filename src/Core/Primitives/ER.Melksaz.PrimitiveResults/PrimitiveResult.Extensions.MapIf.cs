using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{
    #region " ===================== CORE ===================== "

    /// <summary>
    /// Core implementation for MapIf operations.
    /// </summary>
    private static PrimitiveResult<TOut> MapIfCore<TValue, TOut>(
        PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrue,
        Func<TValue, TOut> ifFalse)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(ifTrue);
        ArgumentNullException.ThrowIfNull(ifFalse);

        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var value = src.Value;

        var result = predicate(value) ? ifTrue(value) : ifFalse(value);

        if (result is null && default(TOut) is null)
            return PrimitiveResult.Failure<TOut>(NullMapperResultError.Value);

        return PrimitiveResult.Success(result);
    }

    #endregion

    #region " ===================== MAPIF ===================== "
    /// <summary>
    /// Maps the value of the current <see cref="PrimitiveResult{TValue}"/> into a new value
    /// based on the result of the specified predicate.
    /// If the source result is a failure, the errors are propagated and no mapping is executed.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static PrimitiveResult<TOut> MapIf<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrue,
        Func<TValue, TOut> ifFalse)
        => MapIfCore(src, predicate, ifTrue, ifFalse);


    /// <summary>
    /// Maps the value of the current <see cref="PrimitiveResult{TValue}"/> into another
    /// <see cref="PrimitiveResult{TOut}"/> depending on the predicate result.
    /// If the source result is a failure, its errors are propagated.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static PrimitiveResult<TOut> MapIf<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrue,
        Func<TValue, PrimitiveResult<TOut>> ifFalse)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(ifTrue);
        ArgumentNullException.ThrowIfNull(ifFalse);

        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var value = src.Value;
        return predicate(value) ? ifTrue(value) : ifFalse(value);
    }


    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into a new value using <see cref="ValueTask"/> based mappers.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<TOut>> ifTrue,
        Func<TValue, ValueTask<TOut>> ifFalse)
    {
        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var value = src.Value;
        var result = predicate(value)
            ? await ifTrue(value)
            : await ifFalse(value);

        if (result is null && default(TOut) is null)
            return PrimitiveResult.Failure<TOut>("MapIf.Null", "Mapper returned null");

        return PrimitiveResult.Success(result);
    }


    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrue,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalse)
    {
        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var value = src.Value;

        return predicate(value)
            ? await ifTrue(value)
            : await ifFalse(value);
    }


    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into a new value using <see cref="Task"/> based mappers.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TOut>> ifTrue,
        Func<TValue, Task<TOut>> ifFalse)
    {
        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var value = src.Value;

        var result = predicate(value)
            ? await ifTrue(value)
            : await ifFalse(value);

        if (result is null && default(TOut) is null)
            return PrimitiveResult.Failure<TOut>("MapIf.Null", "Mapper returned null");

        return PrimitiveResult.Success(result);
    }


    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TOut>>> ifTrue,
        Func<TValue, Task<PrimitiveResult<TOut>>> ifFalse)
    {
        if (src.IsFailure)
            return PrimitiveResult.Failure<TOut>(src.Errors);

        var value = src.Value;

        return predicate(value)
            ? await ifTrue(value)
            : await ifFalse(value);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrue,
        Func<TValue, TOut> ifFalse)
    {
        var result = await src;
        return result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrue,
        Func<TValue, PrimitiveResult<TOut>> ifFalse)
    {
        var result = await src;
        return result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<TOut>> ifTrue,
        Func<TValue, ValueTask<TOut>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrue,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TOut>> ifTrue,
        Func<TValue, Task<TOut>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TOut>>> ifTrue,
        Func<TValue, Task<PrimitiveResult<TOut>>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrue,
        Func<TValue, TOut> ifFalse)
    {
        var result = await src;
        return result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrue,
        Func<TValue, PrimitiveResult<TOut>> ifFalse)
    {
        var result = await src;
        return result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<TOut>> ifTrue,
        Func<TValue, ValueTask<TOut>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrue,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TOut>> ifTrue,
        Func<TValue, Task<TOut>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }

    /// <summary>
    /// Asynchronously maps the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TOut}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TOut}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TOut>>> ifTrue,
        Func<TValue, Task<PrimitiveResult<TOut>>> ifFalse)
    {
        var result = await src;
        return await result.MapIf(predicate, ifTrue, ifFalse);
    }
    #endregion
}