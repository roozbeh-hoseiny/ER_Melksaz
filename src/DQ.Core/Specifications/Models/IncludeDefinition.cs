using System.Linq.Expressions;

namespace DQ.Core.Specifications.Models;

/// <summary>
/// Represents an Entity Framework Core navigation include definition.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public sealed record IncludeDefinition<TEntity>
{
    /// <summary>
    /// Gets the navigation expression.
    /// </summary>
    public Expression<Func<TEntity, object>> Expression { get; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="IncludeDefinition{TEntity}"/> class.
    /// </summary>
    /// <param name="expression">
    /// Navigation property expression.
    /// </param>
    public IncludeDefinition(Expression<Func<TEntity, object>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        this.Expression = expression;
    }
}