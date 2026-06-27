namespace ER.Melksaz.PrimitiveResults;

/// <summary>
/// Represents the outcome of a context-driven pipeline where a context object
/// is carried through every step.
/// </summary>
/// <typeparam name="TContext">
/// The type of the carried context.
/// </typeparam>
/// <remarks>
/// <para>
/// This type is intended for middleware-style or request-style flows where a single
/// context instance is passed through a sequence of operations.
/// </para>
/// <para>
/// The context is preserved even when the pipeline fails. This makes it possible to inspect,
/// log, or enrich the context before, during, and after execution.
/// </para>
/// <para>
/// The wrapper itself is immutable, meaning every success/failure state transition produces
/// a new <see cref="ContextResult{TContext}"/> when needed. The carried context instance
/// may still be mutable, which is often desirable in HttpContext-like scenarios.
/// </para>
/// </remarks>
public sealed class ContextResult<TContext>
{
    #region " Properties "
    /// <summary>
    /// Gets the carried context object.
    /// </summary>
    public TContext Context { get; }

    /// <summary>
    /// Gets the current pipeline status.
    /// </summary>
    public PrimitiveResult Status { get; }

    /// <summary>
    /// Gets a value indicating whether the current pipeline state is successful.
    /// </summary>
    public bool IsSuccess => this.Status.IsSuccess;

    /// <summary>
    /// Gets a value indicating whether the current pipeline state is failed.
    /// </summary>
    public bool IsFailure => this.Status.IsFailure;

    /// <summary>
    /// Gets the errors associated with the current failure state.
    /// </summary>
    public IReadOnlyList<PrimitiveError> Errors => this.Status.Errors;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextResult{TContext}"/> class.
    /// </summary>
    /// <param name="context">The carried context.</param>
    /// <param name="status">The current pipeline status.</param> 
    #endregion

    #region " Constructor "
    private ContextResult(TContext context, PrimitiveResult status)
    {
        this.Context = context;
        this.Status = status;
    }
    #endregion

    #region " Factory Methods "

    /// <summary>
    /// Creates a successful <see cref="ContextResult{TContext}"/> for the specified context.
    /// </summary>
    /// <param name="context">The context to carry through the pipeline.</param>
    /// <returns>
    /// A successful <see cref="ContextResult{TContext}"/> instance.
    /// </returns>
    public static ContextResult<TContext> Success(TContext context) =>
        new(context, PrimitiveResult.Success());

    /// <summary>
    /// Creates a failed <see cref="ContextResult{TContext}"/> for the specified context.
    /// </summary>
    /// <param name="context">The context at the point of failure.</param>
    /// <param name="errors">The errors describing the failure.</param>
    /// <returns>
    /// A failed <see cref="ContextResult{TContext}"/> instance.
    /// </returns>
    public static ContextResult<TContext> Failure(TContext context, params PrimitiveError[] errors) =>
        new(context, PrimitiveResult.Failure(errors));

    #endregion

    #region " Bind "

    /// <summary>
    /// Executes a pipeline step that can both transform the context
    /// and indicate success or failure.
    /// </summary>
    /// <param name="step">
    /// A function that receives the current context and returns
    /// a result containing the updated context.
    /// </param>
    /// <returns>
    /// The current failed result if the pipeline has already failed;
    /// otherwise the result produced by the supplied step.
    /// </returns>
    public ContextResult<TContext> Bind(Func<TContext, PrimitiveResult<TContext>> step)
    {
        if (this.IsFailure)
            return this;

        ArgumentNullException.ThrowIfNull(step);

        var result = step(this.Context);

        return result.IsSuccess
            ? Success(result.Value)
            : Failure(this.Context, ToArray(result.Errors));
    }

    /// <summary>
    /// Executes an asynchronous pipeline step that can both transform the context
    /// and indicate success or failure using <see cref="Task"/>.
    /// </summary>
    /// <param name="step">
    /// A function that receives the current context and returns a task
    /// containing a result with the updated context.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous pipeline step.
    /// </returns>
    public async Task<ContextResult<TContext>> Bind(Func<TContext, Task<PrimitiveResult<TContext>>> step)
    {
        if (this.IsFailure)
            return this;

        ArgumentNullException.ThrowIfNull(step);

        var result = await step(this.Context)
            .ConfigureAwait(false);

        return result.IsSuccess
            ? Success(result.Value)
            : Failure(this.Context, ToArray(result.Errors));
    }

    /// <summary>
    /// Executes an asynchronous pipeline step that can both transform the context
    /// and indicate success or failure using <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="step">
    /// A function that receives the current context and returns a value task
    /// containing a result with the updated context.
    /// </param>
    /// <returns>
    /// A value task representing the asynchronous pipeline step.
    /// </returns>
    public async ValueTask<ContextResult<TContext>> Bind(Func<TContext, ValueTask<PrimitiveResult<TContext>>> step)
    {
        if (this.IsFailure)
            return this;

        ArgumentNullException.ThrowIfNull(step);

        var result = await step(this.Context)
            .ConfigureAwait(false);

        return result.IsSuccess
            ? Success(result.Value)
            : Failure(this.Context, ToArray(result.Errors));
    }

    /// <summary>
    /// Executes a synchronous pipeline step that returns a non-generic result.
    /// </summary>
    /// <param name="step">
    /// A function that receives the carried context and returns a <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// The current failed result if already failed; otherwise a new result based on the step outcome.
    /// </returns>
    public ContextResult<TContext> Bind(Func<TContext, PrimitiveResult> step)
    {
        ArgumentNullException.ThrowIfNull(step);

        if (this.IsFailure)
            return this;

        var result = step(this.Context);
        return this.BindCore(result);
    }

    /// <summary>
    /// Executes a synchronous pipeline step that returns a new contextual result.
    /// </summary>
    /// <param name="step">
    /// A function that receives the carried context and returns a new <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// The current failed result if already failed; otherwise the result returned by the step.
    /// </returns>
    public ContextResult<TContext> Bind(Func<TContext, ContextResult<TContext>> step)
    {
        ArgumentNullException.ThrowIfNull(step);

        if (this.IsFailure)
            return this;

        return step(this.Context);
    }

    /// <summary>
    /// Executes an asynchronous pipeline step that returns a non-generic result using <see cref="Task"/>.
    /// </summary>
    /// <param name="step">
    /// A function that receives the carried context and returns a <see cref="Task{TResult}"/>
    /// containing a <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous pipeline step.
    /// </returns>
    public async Task<ContextResult<TContext>> Bind(Func<TContext, Task<PrimitiveResult>> step)
    {
        ArgumentNullException.ThrowIfNull(step);

        if (this.IsFailure)
            return this;

        var result = await step(this.Context).ConfigureAwait(false);
        return this.BindCore(result);
    }

    /// <summary>
    /// Executes an asynchronous pipeline step that returns a new contextual result using <see cref="Task"/>.
    /// </summary>
    /// <param name="step">
    /// A function that receives the carried context and returns a <see cref="Task{TResult}"/>
    /// containing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous pipeline step.
    /// </returns>
    public async Task<ContextResult<TContext>> Bind(Func<TContext, Task<ContextResult<TContext>>> step)
    {
        if (this.IsFailure)
            return this;

        return await step(this.Context).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes an asynchronous pipeline step that returns a non-generic result using <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="step">
    /// A function that receives the carried context and returns a <see cref="ValueTask{TResult}"/>
    /// containing a <see cref="PrimitiveResult"/>.
    /// </param>
    /// <returns>
    /// A value task representing the asynchronous pipeline step.
    /// </returns>
    public async ValueTask<ContextResult<TContext>> Bind(Func<TContext, ValueTask<PrimitiveResult>> step)
    {
        if (this.IsFailure)
            return this;

        var result = await step(this.Context).ConfigureAwait(false);
        return this.BindCore(result);
    }

    /// <summary>
    /// Executes an asynchronous pipeline step that returns a new contextual result using <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="step">
    /// A function that receives the carried context and returns a <see cref="ValueTask{TResult}"/>
    /// containing a <see cref="ContextResult{TContext}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the asynchronous pipeline step.
    /// </returns>
    public async ValueTask<ContextResult<TContext>> Bind(Func<TContext, ValueTask<ContextResult<TContext>>> step)
    {
        if (this.IsFailure)
            return this;

        return await step(this.Context).ConfigureAwait(false);
    }

    #endregion

    #region " Map "

    /// <summary>
    /// Projects the carried context into a plain output value.
    /// </summary>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="mapper">
    /// A function that transforms the context into a value.
    /// </param>
    /// <returns>
    /// A successful <see cref="PrimitiveResult{T}"/> containing the mapped value,
    /// or a failed result preserving the current errors.
    /// </returns>
    public PrimitiveResult<TOut> Map<TOut>(Func<TContext, TOut> mapper)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        return mapper(this.Context);
    }

    /// <summary>
    /// Projects the carried context into a result-wrapped output value.
    /// </summary>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="mapper">
    /// A function that transforms the context into a <see cref="PrimitiveResult{T}"/>.
    /// </param>
    /// <returns>
    /// The mapped result if successful; otherwise a failed result preserving current errors.
    /// </returns>
    public PrimitiveResult<TOut> Map<TOut>(Func<TContext, PrimitiveResult<TOut>> mapper)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        return mapper(this.Context);
    }

    /// <summary>
    /// Asynchronously projects the carried context into a plain output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="mapper">
    /// A function that transforms the context into a <see cref="Task{TResult}"/> containing the output value.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous projection.
    /// </returns>
    public async Task<PrimitiveResult<TOut>> Map<TOut>(Func<TContext, Task<TOut>> mapper)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        var value = await mapper(this.Context).ConfigureAwait(false);
        return this.MapCore(value);
    }

    /// <summary>
    /// Asynchronously projects the carried context into a result-wrapped output value using <see cref="Task"/>.
    /// </summary>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="mapper">
    /// A function that transforms the context into a <see cref="Task{TResult}"/>
    /// containing a <see cref="PrimitiveResult{T}"/>.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous projection.
    /// </returns>
    public async Task<PrimitiveResult<TOut>> Map<TOut>(Func<TContext, Task<PrimitiveResult<TOut>>> mapper)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        return await mapper(this.Context).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously projects the carried context into a plain output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="mapper">
    /// A function that transforms the context into a <see cref="ValueTask{TResult}"/> containing the output value.
    /// </param>
    /// <returns>
    /// A value task representing the asynchronous projection.
    /// </returns>
    public async ValueTask<PrimitiveResult<TOut>> Map<TOut>(Func<TContext, ValueTask<TOut>> mapper)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        var value = await mapper(this.Context).ConfigureAwait(false);
        return this.MapCore(value);
    }

    /// <summary>
    /// Asynchronously projects the carried context into a result-wrapped output value using <see cref="ValueTask"/>.
    /// </summary>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="mapper">
    /// A function that transforms the context into a <see cref="ValueTask{TResult}"/>
    /// containing a <see cref="PrimitiveResult{T}"/>.
    /// </param>
    /// <returns>
    /// A value task representing the asynchronous projection.
    /// </returns>
    public async ValueTask<PrimitiveResult<TOut>> Map<TOut>(Func<TContext, ValueTask<PrimitiveResult<TOut>>> mapper)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        return await mapper(this.Context).ConfigureAwait(false);
    }

    #endregion

    #region " Ensure "

    /// <summary>
    /// Verifies that the carried context satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition that must be satisfied.</param>
    /// <param name="errors">The errors to return if the predicate evaluates to <see langword="false"/>.</param>
    /// <returns>
    /// The current successful result if the predicate passes; otherwise a failed result.
    /// </returns>
    public ContextResult<TContext> Ensure(Func<TContext, bool> predicate, params PrimitiveError[] errors)
    {
        if (this.IsFailure)
            return this;

        return this.EnsureCore(predicate(this.Context), errors);
    }

    /// <summary>
    /// Asynchronously verifies that the carried context satisfies the specified predicate using <see cref="Task"/>.
    /// </summary>
    /// <param name="predicate">
    /// The asynchronous condition that must be satisfied.
    /// </param>
    /// <param name="errors">The errors to return if the predicate evaluates to <see langword="false"/>.</param>
    /// <returns>
    /// A task representing the asynchronous validation operation.
    /// </returns>
    public async Task<ContextResult<TContext>> Ensure(Func<TContext, Task<bool>> predicate, params PrimitiveError[] errors)
    {
        if (this.IsFailure)
            return this;

        var isValid = await predicate(this.Context).ConfigureAwait(false);
        return this.EnsureCore(isValid, errors);
    }

    /// <summary>
    /// Asynchronously verifies that the carried context satisfies the specified predicate using <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="predicate">
    /// The asynchronous condition that must be satisfied.
    /// </param>
    /// <param name="errors">The errors to return if the predicate evaluates to <see langword="false"/>.</param>
    /// <returns>
    /// A value task representing the asynchronous validation operation.
    /// </returns>
    public async ValueTask<ContextResult<TContext>> Ensure(Func<TContext, ValueTask<bool>> predicate, params PrimitiveError[] errors)
    {
        if (this.IsFailure)
            return this;

        var isValid = await predicate(this.Context).ConfigureAwait(false);
        return this.EnsureCore(isValid, errors);
    }

    #endregion

    #region " Tap "

    /// <summary>
    /// Executes a synchronous side-effect action when the pipeline is successful.
    /// </summary>
    /// <param name="action">
    /// The action to execute.
    /// </param>
    /// <returns>
    /// The current <see cref="ContextResult{TContext}"/>.
    /// </returns>
    public ContextResult<TContext> Tap(Action<TContext> action)
    {
        this.TapCore(action);
        return this;
    }

    /// <summary>
    /// Executes an asynchronous side-effect action when the pipeline is successful using <see cref="Task"/>.
    /// </summary>
    /// <param name="action">
    /// The asynchronous action to execute.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous side-effect operation.
    /// </returns>
    public async Task<ContextResult<TContext>> Tap(Func<TContext, Task> action)
    {
        if (this.IsSuccess)
            await action(this.Context).ConfigureAwait(false);

        return this;
    }

    /// <summary>
    /// Executes an asynchronous side-effect action when the pipeline is successful using <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="action">
    /// The asynchronous action to execute.
    /// </param>
    /// <returns>
    /// A value task representing the asynchronous side-effect operation.
    /// </returns>
    public async ValueTask<ContextResult<TContext>> Tap(Func<TContext, ValueTask> action)
    {
        if (this.IsSuccess)
            await action(this.Context).ConfigureAwait(false);

        return this;
    }

    #endregion

    #region " Match "

    /// <summary>
    /// Produces a final value by matching against either the success or failure pipeline state.
    /// </summary>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="onSuccess">
    /// The function to execute when the pipeline is successful.
    /// </param>
    /// <param name="onFailure">
    /// The function to execute when the pipeline has failed.
    /// The failure function receives both the errors and the preserved context.
    /// </param>
    /// <returns>
    /// A value produced by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.
    /// </returns>
    public TOut Match<TOut>(
        Func<TContext, TOut> onSuccess,
        Func<IReadOnlyList<PrimitiveError>, TContext, TOut> onFailure)
    {
        return this.IsSuccess
            ? onSuccess(this.Context)
            : onFailure(this.Errors, this.Context);
    }

    #endregion

    #region " Core Helpers "

    /// <summary>
    /// Converts a non-generic result returned by a pipeline step into a contextual result.
    /// </summary>
    /// <param name="result">The step result.</param>
    /// <returns>
    /// The current instance if the step succeeded; otherwise a failed contextual result with the same context.
    /// </returns>
    private ContextResult<TContext> BindCore(PrimitiveResult result)
    {
        if (this.IsFailure)
            return this;

        return result.IsSuccess
            ? this
            : Failure(this.Context, ToArray(result.Errors));
    }

    /// <summary>
    /// Wraps a mapped value in a successful <see cref="PrimitiveResult{T}"/> or propagates current failure errors.
    /// </summary>
    /// <typeparam name="TOut">The mapped output type.</typeparam>
    /// <param name="value">The mapped value.</param>
    /// <returns>
    /// A successful result containing <paramref name="value"/> when the current state is success;
    /// otherwise a failed result containing the current errors.
    /// </returns>
    private PrimitiveResult<TOut> MapCore<TOut>(TOut value)
    {
        if (this.IsFailure)
            return PrimitiveResult.Failure<TOut>(ToArray(this.Errors));

        return PrimitiveResult.Success(value);
    }

    /// <summary>
    /// Converts a predicate evaluation into either the current successful result or a failed result.
    /// </summary>
    /// <param name="condition">The evaluated condition.</param>
    /// <param name="errors">The errors to apply if the condition is not satisfied.</param>
    /// <returns>
    /// The current instance if the condition is satisfied; otherwise a failed contextual result.
    /// </returns>
    private ContextResult<TContext> EnsureCore(bool condition, PrimitiveError[] errors)
    {
        if (this.IsFailure)
            return this;

        return condition
            ? this
            : Failure(this.Context, errors);
    }

    /// <summary>
    /// Executes a side-effect action only when the pipeline is successful.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    private void TapCore(Action<TContext> action)
    {
        if (this.IsSuccess)
            action(this.Context);
    }

    /// <summary>
    /// Converts an error list into an array.
    /// </summary>
    /// <param name="errors">The source error list.</param>
    /// <returns>
    /// An array containing the provided errors.
    /// </returns>
    private static PrimitiveError[] ToArray(IReadOnlyList<PrimitiveError> errors)
    {
        if (errors is PrimitiveError[] array)
            return array;

        var result = new PrimitiveError[errors.Count];
        for (var i = 0; i < errors.Count; i++)
            result[i] = errors[i];

        return result;
    }

    #endregion
}
