using DQ.Core.Expressions.Visitors;
using System.Linq.Expressions;

namespace DQ.Core.Expressions;

public static class ExpressionParameterReplacer
{
    public static Expression Replace(
        Expression expression,
        ParameterExpression source,
        ParameterExpression target)
    {
        var visitor = new ParameterReplaceVisitor(
                new Dictionary<ParameterExpression, ParameterExpression>
                {
                    [source] = target
                });

        return visitor.Visit(expression)!;
    }

    public static TExpression Replace<TExpression>(
        TExpression expression,
        IReadOnlyDictionary<ParameterExpression, ParameterExpression> map)
        where TExpression : Expression
    {
        var visitor = new ParameterReplaceVisitor(map);

        return (TExpression)visitor.Visit(expression)!;
    }
}
