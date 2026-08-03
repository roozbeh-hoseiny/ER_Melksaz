using System.Linq.Expressions;

namespace DQ.Core.Expressions;

/// <summary>
/// Provides helper methods for inspecting and working with LINQ expression trees.
///
/// <para>
/// This class is primarily used by the Specification and Dynamic Projection
/// infrastructure to extract metadata from strongly typed lambda expressions.
/// </para>
///
/// <para>
/// Typical use cases include:
/// <list type="bullet">
/// <item>
/// Extracting the <see cref="MemberExpression"/> from an expression such as
/// <c>x => x.Name</c>.
/// </item>
/// <item>
/// Resolving nested property paths such as
/// <c>x => x.Address.City</c>
/// into the string <c>"Address.City"</c>.
/// </item>
/// <item>
/// Validating that an expression represents a property or field access.
/// </item>
/// </list>
/// </para>
///
/// <para>
/// These helper methods never compile the supplied expression.
/// They operate directly on the expression tree, making them suitable
/// for use with LINQ providers such as Entity Framework Core.
/// </para>
/// </summary>
public static class ExpressionHelper
{
    /// <summary>
    /// Extracts the <see cref="MemberExpression"/> represented by the specified
    /// property access expression.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type that owns the member.
    /// </typeparam>
    /// <typeparam name="TProperty">
    /// The member type.
    /// </typeparam>
    /// <param name="expression">
    /// A lambda expression representing a property or field access.
    /// Examples:
    /// <code>
    /// x => x.Name
    /// x => x.Address.City
    /// </code>
    /// </param>
    /// <returns>
    /// The extracted <see cref="MemberExpression"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the supplied expression does not represent
    /// a member access.
    /// </exception>
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

    /// <summary>
    /// Resolves the full property path represented by the specified expression.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <typeparam name="TProperty">
    /// The property type.
    /// </typeparam>
    /// <param name="expression">
    /// A lambda expression representing a property path.
    /// Examples:
    /// <code>
    /// x => x.Name
    /// x => x.Address.City
    /// x => x.Address.Country.Code
    /// </code>
    /// </param>
    /// <returns>
    /// A dot-separated property path.
    /// Examples:
    /// <code>
    /// Name
    /// Address.City
    /// Address.Country.Code
    /// </code>
    /// </returns>
    /// <remarks>
    /// This method traverses the expression tree from the leaf member
    /// to the root parameter and constructs the complete navigation path.
    /// The returned path is commonly used by the Dynamic Projection engine
    /// when building projection trees.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the supplied expression does not represent
    /// a valid member access.
    /// </exception>
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
