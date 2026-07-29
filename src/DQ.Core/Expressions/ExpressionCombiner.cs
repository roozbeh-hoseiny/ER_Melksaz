using System.Linq.Expressions;

namespace DQ.Core.Expressions;

public static class ExpressionCombiner
{
    public static Expression<Func<T, bool>> CombineAnd<T>(IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        return expressions.Aggregate((a, b) => a.And(b));
    }

    public static Expression<Func<T, bool>> CombineOr<T>(IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        return expressions.Aggregate((a, b) => a.Or(b));
    }
}