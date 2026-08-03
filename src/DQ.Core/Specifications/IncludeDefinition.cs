using System.Linq.Expressions;

namespace DQ.Core.Specifications;

/// <summary>
/// Represents an Entity Framework navigation include expression.
/// </summary>
internal sealed record IncludeDefinition
{
    /// <summary>
    /// Gets the include expression.
    /// </summary>
    public LambdaExpression Expression { get; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="IncludeDefinition"/> class.
    /// </summary>
    public IncludeDefinition(LambdaExpression expression)
    {
        this.Expression = expression;
    }



}