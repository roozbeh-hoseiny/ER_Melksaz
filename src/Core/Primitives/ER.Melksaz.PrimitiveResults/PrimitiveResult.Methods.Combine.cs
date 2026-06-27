using ER.Melksaz.PrimitiveResults;
using System.Diagnostics.CodeAnalysis;

namespace ER.Melksaz.PrimitiveResults;

public sealed partial class PrimitiveResult
{
    /// <summary>
    /// Combines multiple <see cref="PrimitiveResult"/> instances into a single result.
    /// </summary>
    /// <param name="results">The results to combine.</param>
    /// <returns>
    /// A successful result when all supplied results are successful;
    /// otherwise, a failed result containing all collected errors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="results"/> is null.
    /// </exception>
    [return: NotNull]
    public static PrimitiveResult Combine(params PrimitiveResult[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var failrureResults = results
            .Where(x => x.IsFailure)
            .ToArray();

        if (failrureResults.Length > 0)
        {
            return PrimitiveResult.Failure(
                failrureResults
                    .SelectMany(x => x.Errors)
                    .ToArray());
        }

        return PrimitiveResult.Success();
    }

    /// <summary>
    /// Combines multiple <see cref="PrimitiveResult{TValue}"/> instances
    /// into a single result.
    /// </summary>
    /// <typeparam name="TValue">The wrapped value type.</typeparam>
    /// <param name="results">The results to combine.</param>
    /// <returns>
    /// A failed result containing all collected errors when any supplied
    /// result fails; otherwise, the first successful result.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="results"/> is null.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when no results are supplied.
    /// </exception>
    [return: NotNull]
    public static PrimitiveResult Combine<TValue>(params PrimitiveResult<TValue>[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var failrureResults = results
            .Where(x => x.IsFailure)
            .ToArray();

        if (failrureResults.Length > 0)
        {
            return PrimitiveResult.Failure(
                failrureResults
                    .SelectMany(x => x.Errors)
                    .ToArray());
        }

        return PrimitiveResult.Success();
    }

    /// <summary>
    /// Asynchronously combines multiple
    /// <see cref="PrimitiveResult"/> instances into a single result.
    /// </summary>
    /// <param name="results">The async results to combine.</param>
    /// <returns>
    /// A successful result when all supplied results are successful;
    /// otherwise, a failed result containing all collected errors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="results"/> is null.
    /// </exception>
    [return: NotNull]
    public static async ValueTask<PrimitiveResult> Combine(params ValueTask<PrimitiveResult>[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        List<PrimitiveResult> list = new(results.Length);

        foreach (var f in results)
        {
            list.Add(await f.ConfigureAwait(false));
        }

        return Combine(list.ToArray());
    }

    /// <summary>
    /// Asynchronously combines multiple
    /// <see cref="PrimitiveResult{TValue}"/> instances into a single result.
    /// </summary>
    /// <typeparam name="TValue">The wrapped value type.</typeparam>
    /// <param name="results">The async results to combine.</param>
    /// <returns>
    /// A failed result containing all collected errors when any supplied
    /// result fails; otherwise, the first successful result.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="results"/> is null.
    /// </exception>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when no results are supplied.
    /// </exception>
    [return: NotNull]
    public static async ValueTask<PrimitiveResult> Combine<TValue>(params ValueTask<PrimitiveResult<TValue>>[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        List<PrimitiveResult<TValue>> list = new(results.Length);

        foreach (var f in results)
        {
            list.Add(await f.ConfigureAwait(false));
        }

        return Combine(list.ToArray());
    }

    /// <summary>
    /// Combines multiple <see cref="PrimitiveResult{TValue}"/> instances
    /// into a single result containing all successful values.
    /// </summary>
    /// <typeparam name="TValue">The wrapped value type.</typeparam>
    /// <param name="results">The results to combine.</param>
    /// <returns>
    /// A successful result containing all values when every supplied result
    /// is successful; otherwise, a failed result containing all collected
    /// errors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="results"/> is null.
    /// </exception>
    [return: NotNull]
    public static PrimitiveResult<IEnumerable<TValue>> CombineAll<TValue>(
        params PrimitiveResult<TValue>[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var failrureResults = results
            .Where(x => x.IsFailure)
            .ToArray();

        if (failrureResults.Length > 0)
        {
            return PrimitiveResult.Failure<IEnumerable<TValue>>(
                failrureResults
                    .SelectMany(x => x.Errors)
                    .ToArray());
        }

        return PrimitiveResult.Success(
            results.Select(x => x.Value));
    }

    /// <summary>
    /// Asynchronously combines multiple
    /// <see cref="PrimitiveResult{TValue}"/> instances into a single result
    /// containing all successful values.
    /// </summary>
    /// <typeparam name="TValue">The wrapped value type.</typeparam>
    /// <param name="results">The async results to combine.</param>
    /// <returns>
    /// A successful result containing all values when every supplied result
    /// is successful; otherwise, a failed result containing all collected
    /// errors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="results"/> is null.
    /// </exception>
    [return: NotNull]
    public static async ValueTask<PrimitiveResult<IEnumerable<TValue>>> CombineAll<TValue>(
        params ValueTask<PrimitiveResult<TValue>>[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        List<PrimitiveResult<TValue>> list = new(results.Length);

        foreach (var f in results)
        {
            list.Add(await f.ConfigureAwait(false));
        }

        return CombineAll(list.ToArray());
    }
}