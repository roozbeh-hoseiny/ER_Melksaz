using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents an ordering rule for an entity query.
/// </summary>
internal sealed record OrderDefinition
{
    /// <summary>
    /// Gets the ordering expression.
    /// </summary>
    public LambdaExpression Expression { get; }


    /// <summary>
    /// Gets whether the ordering is descending.
    /// </summary>
    public bool Descending { get; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="OrderDefinition"/> class.
    /// </summary>
    public OrderDefinition(LambdaExpression expression, bool descending)
    {
        this.Expression = expression;
        this.Descending = descending;
    }
}