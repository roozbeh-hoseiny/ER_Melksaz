using System.Linq.Expressions;

namespace DQ.Core.Expressions;

public static class ExpressionHelper
{
    public static MemberExpression GetMemberExpression<TEntity, TProperty>(
        Expression<Func<TEntity, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member)
        {
            return member;
        }

        if (expression.Body is UnaryExpression unary &&
            unary.Operand is MemberExpression unaryMember)
        {
            return unaryMember;
        }

        throw new InvalidOperationException(
            $"Expression '{expression}' does not reference a member.");
    }

    public static string GetMemberPath<TEntity, TProperty>(
        Expression<Func<TEntity, TProperty>> expression)
    {
        var member = GetMemberExpression(expression);

        var names = new Stack<string>();

        Expression? current = member;

        while (current is MemberExpression currentMember)
        {
            names.Push(currentMember.Member.Name);
            current = currentMember.Expression;
        }

        return string.Join(".", names);
    }
}