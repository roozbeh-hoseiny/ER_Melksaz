using System.Linq.Expressions;

namespace DQ.Core.Expressions;

public static class ExpressionCombiner
{
    public static Expression<Func<T, bool>> CombineAnd<T>(
        IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        ArgumentNullException.ThrowIfNull(expressions);

        var list = expressions.ToList();

        if (list.Count == 0)
            throw new InvalidOperationException("No expressions supplied.");

        return list.Aggregate((left, right) => left.And(right));
    }

    public static Expression<Func<T, bool>> CombineOr<T>(
        IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        ArgumentNullException.ThrowIfNull(expressions);

        var list = expressions.ToList();

        if (list.Count == 0)
            throw new InvalidOperationException("No expressions supplied.");

        return list.Aggregate((left, right) => left.Or(right));
    }
}
