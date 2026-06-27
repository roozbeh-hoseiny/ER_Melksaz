using ER.Melksaz.PrimitiveResults;
using System.Diagnostics.CodeAnalysis;

namespace ER.Melksaz.PrimitiveResults;

public sealed partial class PrimitiveResult
{
    /// <summary>
    /// Executes a bind operation for every item in the source collection
    /// and combines all successful results into a single result.
    /// </summary>
    /// <typeparam name="TValue">The source value type.</typeparam>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="src">The source collection.</param>
    /// <param name="func">
    /// The bind function executed for each source item.
    /// The current item and its zero-based index are provided.
    /// </param>
    /// <param name="iterationStrategy">
    /// Controls how iteration behaves when failures occur.
    /// </param>
    /// <returns>
    /// A successful result containing all mapped values when all operations
    /// succeed; otherwise, a failed result containing collected errors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/> or <paramref name="func"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the bind function returns null.
    /// </exception>
    [return: NotNull]
    public static PrimitiveResult<IEnumerable<TOut>> BindAll<TValue, TOut>(
        IEnumerable<TValue> src,
        Func<TValue, int, PrimitiveResult<TOut>> func,
        BindAllIterationStrategy iterationStrategy =
            BindAllIterationStrategy.BreakOnFirstError)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(func);

        IList<TValue> source =
            src as IList<TValue> ??
            src.ToList();

        List<PrimitiveResult<TOut>> resultList = new(source.Count);

        for (var i = 0; i < source.Count; i++)
        {
            PrimitiveResult<TOut> currentResult = func(source[i], i);

            if (currentResult is null)
            {
                throw new InvalidOperationException("The bind function returned null.");
            }

            resultList.Add(currentResult);

            if (currentResult.IsFailure &&
                iterationStrategy.HasFlag(
                    BindAllIterationStrategy.BreakOnFirstError))
            {
                break;
            }
        }

        if (iterationStrategy.HasFlag(
            BindAllIterationStrategy.GoToLastAndIgnoreErrors))
        {
            _ = resultList.RemoveAll(x => x.IsFailure);
        }

        return CombineAll(resultList.ToArray());
    }

    /// <summary>
    /// Asynchronously executes a bind operation for every item in the source
    /// collection and combines all successful results into a single result.
    /// </summary>
    /// <typeparam name="TValue">The source value type.</typeparam>
    /// <typeparam name="TOut">The output value type.</typeparam>
    /// <param name="src">The source collection.</param>
    /// <param name="func">
    /// The asynchronous bind function executed for each source item.
    /// The current item and its zero-based index are provided.
    /// </param>
    /// <param name="iterationStrategy">
    /// Controls how iteration behaves when failures or cancellation occur.
    /// </param>
    /// <param name="cancellationToken">
    /// A token used to observe cancellation requests.
    /// </param>
    /// <returns>
    /// A successful result containing all mapped values when all operations
    /// succeed; otherwise, a failed result containing collected errors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="src"/> or <paramref name="func"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the bind function returns null.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Thrown when cancellation is requested and
    /// <see cref="BindAllIterationStrategy.ThrowExceptionOnCancellationRequested"/>
    /// is enabled.
    /// </exception>
    [return: NotNull]
    public static async ValueTask<PrimitiveResult<IEnumerable<TOut>>> BindAll<TValue, TOut>(
        IEnumerable<TValue> src,
        Func<TValue, int, ValueTask<PrimitiveResult<TOut>>> func,
        BindAllIterationStrategy iterationStrategy =
            BindAllIterationStrategy.BreakOnFirstError,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(func);

        IList<TValue> source =
            src as IList<TValue> ??
            src.ToList();

        List<PrimitiveResult<TOut>> resultList =
            new(source.Count);

        for (var i = 0; i < source.Count; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                if (iterationStrategy.HasFlag(
                    BindAllIterationStrategy.ThrowExceptionOnCancellationRequested))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                break;
            }

            PrimitiveResult<TOut> currentResult =
                await func(source[i], i)
                    .ConfigureAwait(false);

            if (currentResult is null)
            {
                throw new InvalidOperationException(
                    "The bind function returned null.");
            }

            resultList.Add(currentResult);

            if (currentResult.IsFailure &&
                iterationStrategy.HasFlag(
                    BindAllIterationStrategy.BreakOnFirstError))
            {
                break;
            }
        }

        if (iterationStrategy.HasFlag(
            BindAllIterationStrategy.GoToLastAndIgnoreErrors))
        {
            _ = resultList.RemoveAll(x => x.IsFailure);
        }

        return CombineAll(resultList.ToArray());
    }
}