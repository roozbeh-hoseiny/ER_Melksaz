using System.Linq.Expressions;

namespace DQ.Core.Expressions.Visitors;

internal sealed class ParameterReplaceVisitor : ExpressionVisitor
{
    private readonly IReadOnlyDictionary<ParameterExpression, ParameterExpression> _map;

    public ParameterReplaceVisitor(IReadOnlyDictionary<ParameterExpression, ParameterExpression> map)
    {
        this._map = map;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (this._map.TryGetValue(node, out var replacement))
        {
            return replacement;
        }

        return base.VisitParameter(node);
    }
}
