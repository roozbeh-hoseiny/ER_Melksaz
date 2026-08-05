using System.Linq.Expressions;

namespace DQ.Core.Expressions.Visitors;

/// <summary>
/// Replaces one or more parameter expressions while traversing an expression tree.
/// </summary>
/// <remarks>
/// This visitor is primarily used when combining multiple lambda expressions
/// into a single expression tree. Since each lambda owns its own
/// <see cref="ParameterExpression"/> instances, all parameters must be
/// normalized to the same instance before the expressions can be combined.
///
/// <para>
/// Only <see cref="ParameterExpression"/> nodes are replaced.
/// All other nodes remain unchanged.
/// </para>
/// </remarks>
internal sealed class ParameterReplaceVisitor : ExpressionVisitor
{
    #region Fields

    private readonly IReadOnlyDictionary<ParameterExpression, ParameterExpression> _map;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ParameterReplaceVisitor"/> class.
    /// </summary>
    /// <param name="map">
    /// A mapping between source parameters and their replacement parameters.
    /// </param>
    public ParameterReplaceVisitor(IReadOnlyDictionary<ParameterExpression, ParameterExpression> map)
    {
        ArgumentNullException.ThrowIfNull(map);

        this._map = map;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (this._map.TryGetValue(node, out var replacement))
        {
            return replacement;
        }

        return node;
    }

    #endregion
}