using System.Linq.Expressions;

namespace DQ.Core.Expressions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var leftBody =
            ExpressionParameterReplacer.Replace(
                left.Body,
                left.Parameters[0],
                parameter);

        var rightBody =
            ExpressionParameterReplacer.Replace(
                right.Body,
                right.Parameters[0],
                parameter);

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(leftBody, rightBody),
            parameter);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var leftBody =
            ExpressionParameterReplacer.Replace(
                left.Body,
                left.Parameters[0],
                parameter);

        var rightBody =
            ExpressionParameterReplacer.Replace(
                right.Body,
                right.Parameters[0],
                parameter);

        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(leftBody, rightBody),
            parameter);
    }

    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var body =
            ExpressionParameterReplacer.Replace(
                expression.Body,
                expression.Parameters[0],
                parameter);

        return Expression.Lambda<Func<T, bool>>(
            Expression.Not(body),
            parameter);
    }
}
