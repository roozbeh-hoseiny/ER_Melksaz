namespace DQ.Core.Expressions;

/// <summary>
/// Represents a cache key generated from an expression tree.
/// </summary>
/// <remarks>
/// Expression instances cannot safely be used as cache keys because
/// two equivalent expressions can be different CLR objects.
///
/// This type provides a stable representation that can be used by
/// expression and projection caches.
/// </remarks>
public readonly record struct ExpressionCacheKey
{
    /// <summary>
    /// Gets the normalized expression value.
    /// </summary>
    public string Value { get; }


    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ExpressionCacheKey"/> struct.
    /// </summary>
    /// <param name="value">
    /// The normalized expression representation.
    /// </param>
    public ExpressionCacheKey(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        this.Value = value;
    }

    public override string ToString()
    {
        return this.Value;
    }
}