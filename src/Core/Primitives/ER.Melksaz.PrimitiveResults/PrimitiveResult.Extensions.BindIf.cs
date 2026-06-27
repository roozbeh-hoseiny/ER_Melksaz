using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.PrimitiveResults;

public static partial class PrimitiveResultExtensions
{

    #region " ===================== BINDIF ===================== "
    /// <summary>
    /// Binds the value of the current <see cref="PrimitiveResult{TValue}"/> into a new value
    /// based on the result of the specified predicate.
    /// If the source result is a failure, the errors are propagated and no mapping is executed.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static PrimitiveResult<TValue> BindIf<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, TValue> ifTrue,
        Func<TValue, TValue> ifFalse) => src.MapIf(predicate, ifTrue, ifFalse);


    /// <summary>
    /// Binds the value of the current <see cref="PrimitiveResult{TValue}"/> into another
    /// <see cref="PrimitiveResult{TValue}"/> depending on the predicate result.
    /// If the source result is a failure, its errors are propagated.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static PrimitiveResult<TValue> BindIf<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> ifTrue,
        Func<TValue, PrimitiveResult<TValue>> ifFalse) => src.MapIf(predicate, ifTrue, ifFalse);


    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into a new value using <see cref="ValueTask"/> based mappers.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<TValue>> ifTrue,
        Func<TValue, ValueTask<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);


    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> ifTrue,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);


    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into a new value using <see cref="Task"/> based mappers.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> ifTrue,
        Func<TValue, Task<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);


    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TValue>>> ifTrue,
        Func<TValue, Task<PrimitiveResult<TValue>>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, TValue> ifTrue,
        Func<TValue, TValue> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> ifTrue,
        Func<TValue, PrimitiveResult<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<TValue>> ifTrue,
        Func<TValue, ValueTask<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> ifTrue,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> ifTrue,
        Func<TValue, Task<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TValue>>> ifTrue,
        Func<TValue, Task<PrimitiveResult<TValue>>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, TValue> ifTrue,
        Func<TValue, TValue> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> ifTrue,
        Func<TValue, PrimitiveResult<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<TValue>> ifTrue,
        Func<TValue, ValueTask<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> ifTrue,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);
    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> ifTrue,
        Func<TValue, Task<TValue>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously binds the value of the current <see cref="PrimitiveResult{TValue}"/>
    /// into another <see cref="PrimitiveResult{TValue}"/> using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the source result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="src">The source result.</param>
    /// <param name="predicate">Determines which mapping function should be executed.</param>
    /// <param name="ifTrue">Executed when the predicate evaluates to <c>true</c>.</param>
    /// <param name="ifFalse">Executed when the predicate evaluates to <c>false</c>.</param>
    /// <returns>A new <see cref="PrimitiveResult{TValue}"/> containing the mapped value.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TValue>>> ifTrue,
        Func<TValue, Task<PrimitiveResult<TValue>>> ifFalse) => await src.MapIf(predicate, ifTrue, ifFalse).ConfigureAwait(false);
    #endregion
}