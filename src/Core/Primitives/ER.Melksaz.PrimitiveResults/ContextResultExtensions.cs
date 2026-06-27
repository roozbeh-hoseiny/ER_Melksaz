namespace ER.Melksaz.PrimitiveResults;

/// <summary>
/// Provides fluent extension methods for asynchronous sources of
/// <see cref="ContextResult{TContext}"/>, allowing pipelines to continue
/// seamlessly when a step returns <see cref="Task{TResult}"/> or
/// <see cref="ValueTask{TResult}"/>.
/// </summary>
/// <remarks>
/// <para>
/// These extensions are intended to complement the instance methods defined on
/// <see cref="ContextResult{TContext}"/>. Since <see cref="Task{TResult}"/> and
/// <see cref="ValueTask{TResult}"/> cannot expose the instance methods of
/// <see cref="ContextResult{TContext}"/>, these helpers allow fluent chaining
/// without forcing callers to manually await each intermediate step.
/// </para>
/// <para>
/// The extensions preserve the short-circuiting semantics of
/// <see cref="ContextResult{TContext}"/> by first awaiting the asynchronous
/// source and then delegating to the corresponding instance method.
/// </para>
/// </remarks>
public static class ContextResultExtensions
{
    #region " Task<ContextResult<TContext>> - Bind "

    /// <summary>
    /// Awaits the asynchronous source and executes a synchronous bind step when the
    /// source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// A synchronous function that performs the next pipeline step and returns a
    /// <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Bind<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, PrimitiveResult> step)
    {
        var result = await source.ConfigureAwait(false);
        return result.Bind(step);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes a synchronous contextual bind step
    /// when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// A synchronous function that performs the next pipeline step and returns a
    /// new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Bind<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ContextResult<TContext>> step)
    {
        var result = await source.ConfigureAwait(false);
        return result.Bind(step);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous bind step returning
    /// <see cref="Task{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Bind<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task<PrimitiveResult>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous contextual bind step
    /// returning <see cref="Task{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Bind<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task<ContextResult<TContext>>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous bind step returning
    /// <see cref="ValueTask{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Bind<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask<PrimitiveResult>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous contextual bind step
    /// returning <see cref="ValueTask{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Bind<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask<ContextResult<TContext>>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    #endregion

    #region " Task<ContextResult<TContext>> - Map "

    /// <summary>
    /// Awaits the asynchronous source and maps a successful context into an output value.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// A synchronous mapping function that converts the context into an output value.
    /// </param>
    /// <returns>
    /// A task representing the mapped result.
    /// </returns>
    public static async Task<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, TOut> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return result.Map(mapper);
    }

    /// <summary>
    /// Awaits the asynchronous source and maps a successful context into a result-wrapped output value.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// A synchronous mapping function that converts the context into a
    /// <see cref="PrimitiveResult{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task representing the mapped result.
    /// </returns>
    public static async Task<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, PrimitiveResult<TOut>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return result.Map(mapper);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// an output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into an output value.
    /// </param>
    /// <returns>
    /// A task representing the mapped result.
    /// </returns>
    public static async Task<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task<TOut>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// a result-wrapped output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into a
    /// <see cref="PrimitiveResult{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task representing the mapped result.
    /// </returns>
    public static async Task<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task<PrimitiveResult<TOut>>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// an output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into an output value.
    /// </param>
    /// <returns>
    /// A task representing the mapped result.
    /// </returns>
    public static async Task<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask<TOut>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// a result-wrapped output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into a
    /// <see cref="PrimitiveResult{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task representing the mapped result.
    /// </returns>
    public static async Task<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask<PrimitiveResult<TOut>>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    #endregion

    #region " Task<ContextResult<TContext>> - Ensure "

    /// <summary>
    /// Awaits the asynchronous source and ensures that the successful context satisfies
    /// the specified synchronous predicate.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="predicate">
    /// The predicate that must evaluate to <see langword="true"/> for the pipeline to continue.
    /// </param>
    /// <param name="errors">
    /// The errors to attach if the predicate evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Ensure<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, bool> predicate,
        params PrimitiveError[] errors)
    {
        var result = await source.ConfigureAwait(false);
        return result.Ensure(predicate, errors);
    }

    /// <summary>
    /// Awaits the asynchronous source and ensures that the successful context satisfies
    /// the specified asynchronous predicate returning <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="predicate">
    /// An asynchronous predicate that must evaluate to <see langword="true"/> for the
    /// pipeline to continue.
    /// </param>
    /// <param name="errors">
    /// The errors to attach if the predicate evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Ensure<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task<bool>> predicate,
        params PrimitiveError[] errors)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Ensure(predicate, errors).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and ensures that the successful context satisfies
    /// the specified asynchronous predicate returning <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="predicate">
    /// An asynchronous predicate that must evaluate to <see langword="true"/> for the
    /// pipeline to continue.
    /// </param>
    /// <param name="errors">
    /// The errors to attach if the predicate evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Ensure<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask<bool>> predicate,
        params PrimitiveError[] errors)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Ensure(predicate, errors).ConfigureAwait(false);
    }

    #endregion

    #region " Task<ContextResult<TContext>> - Tap "

    /// <summary>
    /// Awaits the asynchronous source and executes a synchronous side-effect when the
    /// source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="action">
    /// The side-effect to execute.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Tap<TContext>(
        this Task<ContextResult<TContext>> source,
        Action<TContext> action)
    {
        var result = await source.ConfigureAwait(false);
        return result.Tap(action);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous side-effect returning
    /// <see cref="Task"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="action">
    /// The asynchronous side-effect to execute.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Tap<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task> action)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Tap(action).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous side-effect returning
    /// <see cref="ValueTask"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="action">
    /// The asynchronous side-effect to execute.
    /// </param>
    /// <returns>
    /// A task representing the continuation of the pipeline.
    /// </returns>
    public static async Task<ContextResult<TContext>> Tap<TContext>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask> action)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Tap(action).ConfigureAwait(false);
    }

    #endregion

    #region " Task<ContextResult<TContext>> - Match "

    /// <summary>
    /// Awaits the asynchronous source and transforms the final successful or failed state
    /// into an output value.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the final output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="onSuccess">
    /// The function to execute when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The function to execute when the result is failed.
    /// </param>
    /// <returns>
    /// A task representing the final matched output.
    /// </returns>
    public static async Task<TOut> Match<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, TOut> onFailure)
    {
        var result = await source.ConfigureAwait(false);
        return result.Match(onSuccess, onFailure);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously transforms the final successful
    /// or failed state into an output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the final output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="onSuccess">
    /// The asynchronous function to execute when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The asynchronous function to execute when the result is failed.
    /// </param>
    /// <returns>
    /// A task representing the final matched output.
    /// </returns>
    public static async Task<TOut> Match<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, Task<TOut>> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, Task<TOut>> onFailure)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Match(onSuccess, onFailure).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously transforms the final successful
    /// or failed state into an output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the final output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="onSuccess">
    /// The asynchronous function to execute when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The asynchronous function to execute when the result is failed.
    /// </param>
    /// <returns>
    /// A task representing the final matched output.
    /// </returns>
    public static async Task<TOut> Match<TContext, TOut>(
        this Task<ContextResult<TContext>> source,
        Func<TContext, ValueTask<TOut>> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, ValueTask<TOut>> onFailure)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Match(onSuccess, onFailure).ConfigureAwait(false);
    }

    #endregion

    #region " ValueTask<ContextResult<TContext>> - Bind "

    /// <summary>
    /// Awaits the asynchronous source and executes a synchronous bind step when the
    /// source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// A synchronous function that performs the next pipeline step and returns a
    /// <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Bind<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, PrimitiveResult> step)
    {
        var result = await source.ConfigureAwait(false);
        return result.Bind(step);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes a synchronous contextual bind step
    /// when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// A synchronous function that performs the next pipeline step and returns a
    /// new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Bind<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ContextResult<TContext>> step)
    {
        var result = await source.ConfigureAwait(false);
        return result.Bind(step);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous bind step returning
    /// <see cref="Task{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Bind<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task<PrimitiveResult>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous contextual bind step
    /// returning <see cref="Task{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Bind<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task<ContextResult<TContext>>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous bind step returning
    /// <see cref="ValueTask{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Bind<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask<PrimitiveResult>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous contextual bind step
    /// returning <see cref="ValueTask{TResult}"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="step">
    /// An asynchronous function that performs the next pipeline step and returns a
    /// new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Bind<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask<ContextResult<TContext>>> step)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Bind(step).ConfigureAwait(false);
    }

    #endregion

    #region " ValueTask<ContextResult<TContext>> - Map "

    /// <summary>
    /// Awaits the asynchronous source and maps a successful context into an output value.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// A synchronous mapping function that converts the context into an output value.
    /// </param>
    /// <returns>
    /// A value task representing the mapped result.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, TOut> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return result.Map(mapper);
    }

    /// <summary>
    /// Awaits the asynchronous source and maps a successful context into a result-wrapped output value.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// A synchronous mapping function that converts the context into a
    /// <see cref="PrimitiveResult{TOut}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the mapped result.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, PrimitiveResult<TOut>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return result.Map(mapper);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// an output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into an output value.
    /// </param>
    /// <returns>
    /// A value task representing the mapped result.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task<TOut>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// a result-wrapped output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into a
    /// <see cref="PrimitiveResult{TOut}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the mapped result.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task<PrimitiveResult<TOut>>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// an output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into an output value.
    /// </param>
    /// <returns>
    /// A value task representing the mapped result.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask<TOut>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously maps a successful context into
    /// a result-wrapped output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the mapped output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="mapper">
    /// An asynchronous mapping function that converts the context into a
    /// <see cref="PrimitiveResult{TOut}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the mapped result.
    /// </returns>
    public static async ValueTask<PrimitiveResult<TOut>> Map<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask<PrimitiveResult<TOut>>> mapper)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Map(mapper).ConfigureAwait(false);
    }

    #endregion

    #region " ValueTask<ContextResult<TContext>> - Ensure "

    /// <summary>
    /// Awaits the asynchronous source and ensures that the successful context satisfies
    /// the specified synchronous predicate.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="predicate">
    /// The predicate that must evaluate to <see langword="true"/> for the pipeline to continue.
    /// </param>
    /// <param name="errors">
    /// The errors to attach if the predicate evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Ensure<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, bool> predicate,
        params PrimitiveError[] errors)
    {
        var result = await source.ConfigureAwait(false);
        return result.Ensure(predicate, errors);
    }

    /// <summary>
    /// Awaits the asynchronous source and ensures that the successful context satisfies
    /// the specified asynchronous predicate returning <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="predicate">
    /// An asynchronous predicate that must evaluate to <see langword="true"/> for the
    /// pipeline to continue.
    /// </param>
    /// <param name="errors">
    /// The errors to attach if the predicate evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Ensure<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task<bool>> predicate,
        params PrimitiveError[] errors)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Ensure(predicate, errors).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and ensures that the successful context satisfies
    /// the specified asynchronous predicate returning <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="predicate">
    /// An asynchronous predicate that must evaluate to <see langword="true"/> for the
    /// pipeline to continue.
    /// </param>
    /// <param name="errors">
    /// The errors to attach if the predicate evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Ensure<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask<bool>> predicate,
        params PrimitiveError[] errors)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Ensure(predicate, errors).ConfigureAwait(false);
    }

    #endregion

    #region " ValueTask<ContextResult<TContext>> - Tap "

    /// <summary>
    /// Awaits the asynchronous source and executes a synchronous side-effect when the
    /// source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="action">
    /// The side-effect to execute.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Tap<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Action<TContext> action)
    {
        var result = await source.ConfigureAwait(false);
        return result.Tap(action);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous side-effect returning
    /// <see cref="Task"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="action">
    /// The asynchronous side-effect to execute.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Tap<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task> action)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Tap(action).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and executes an asynchronous side-effect returning
    /// <see cref="ValueTask"/> when the source result is successful.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="action">
    /// The asynchronous side-effect to execute.
    /// </param>
    /// <returns>
    /// A value task representing the continuation of the pipeline.
    /// </returns>
    public static async ValueTask<ContextResult<TContext>> Tap<TContext>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask> action)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Tap(action).ConfigureAwait(false);
    }

    #endregion

    #region " ValueTask<ContextResult<TContext>> - Match "

    /// <summary>
    /// Awaits the asynchronous source and transforms the final successful or failed state
    /// into an output value.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the final output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="onSuccess">
    /// The function to execute when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The function to execute when the result is failed.
    /// </param>
    /// <returns>
    /// A value task representing the final matched output.
    /// </returns>
    public static async ValueTask<TOut> Match<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, TOut> onFailure)
    {
        var result = await source.ConfigureAwait(false);
        return result.Match(onSuccess, onFailure);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously transforms the final successful
    /// or failed state into an output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the final output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="onSuccess">
    /// The asynchronous function to execute when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The asynchronous function to execute when the result is failed.
    /// </param>
    /// <returns>
    /// A value task representing the final matched output.
    /// </returns>
    public static async ValueTask<TOut> Match<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, Task<TOut>> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, Task<TOut>> onFailure)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Match(onSuccess, onFailure).ConfigureAwait(false);
    }

    /// <summary>
    /// Awaits the asynchronous source and asynchronously transforms the final successful
    /// or failed state into an output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context carried through the pipeline.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// The type of the final output value.
    /// </typeparam>
    /// <param name="source">
    /// The asynchronous source producing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <param name="onSuccess">
    /// The asynchronous function to execute when the result is successful.
    /// </param>
    /// <param name="onFailure">
    /// The asynchronous function to execute when the result is failed.
    /// </param>
    /// <returns>
    /// A value task representing the final matched output.
    /// </returns>
    public static async ValueTask<TOut> Match<TContext, TOut>(
        this ValueTask<ContextResult<TContext>> source,
        Func<TContext, ValueTask<TOut>> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, ValueTask<TOut>> onFailure)
    {
        var result = await source.ConfigureAwait(false);
        return await result.Match(onSuccess, onFailure).ConfigureAwait(false);
    }

    #endregion
}
