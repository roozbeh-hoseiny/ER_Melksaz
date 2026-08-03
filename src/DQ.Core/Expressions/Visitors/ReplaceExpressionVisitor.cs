using System.Linq.Expressions;

namespace DQ.Core.Expressions.Visitors;

/// <summary>
/// Replaces a specific expression node with another expression node
/// while traversing an expression tree.
/// </summary>
/// <remarks>
/// This visitor is used when composing and transforming expression trees.
/// Unlike <see cref="ParameterReplaceVisitor"/>, which replaces only
/// parameters, this visitor can replace any matching expression node.
/// </remarks>
internal sealed class ReplaceExpressionVisitor : ExpressionVisitor
{
    #region " Fields "
    private readonly Expression _source;
    private readonly Expression _target;
    #endregion

    #region " Constructors "
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ReplaceExpressionVisitor"/> class.
    /// </summary>
    /// <param name="source">
    /// The expression node to replace.
    /// </param>
    /// <param name="target">
    /// The replacement expression node.
    /// </param>
    public ReplaceExpressionVisitor(Expression source, Expression target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        this._source = source;
        this._target = target;
    }

    #endregion

    #region " Methods "

    /// <inheritdoc />
    public override Expression? Visit(Expression? node)
    {
        if (node == this._source)
        {
            return this._target;
        }

        return base.Visit(node);
    }

    #endregion
}