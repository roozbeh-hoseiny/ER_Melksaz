using System.Linq.Expressions;

namespace DQ.Core.Specifications.Models;

/// <summary>
/// Represents an ordering rule for a specification.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed record OrderDefinition<TEntity>
{
    /// <summary>
    /// Gets the ordering expression.
    /// </summary>
    public LambdaExpression Expression { get; }


    /// <summary>
    /// Gets whether ordering is descending.
    /// </summary>
    public bool Descending { get; }

    /// Initializes a new instance of the
    /// <see cref="OrderDefinition{TEntity}"/> class.
    /// </summary>
    public OrderDefinition(LambdaExpression expression, bool descending)
    {
        ArgumentNullException.ThrowIfNull(expression);

        this.Expression = expression;
        this.Descending = descending;
    }
}