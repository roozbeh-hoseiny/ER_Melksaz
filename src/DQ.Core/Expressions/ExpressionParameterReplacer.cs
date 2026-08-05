using DQ.Core.Expressions.Visitors;
using System.Linq.Expressions;

namespace DQ.Core.Expressions;

/// <summary>
/// Provides helper methods for replacing parameter expressions within
/// an expression tree.
/// </summary>
/// <remarks>
/// This class is commonly used when combining multiple lambda expressions
/// into a single expression. Since every lambda owns its own
/// <see cref="ParameterExpression"/>, all parameter instances must be
/// normalized before the expressions can be merged.
///
/// <para>
/// The methods provided by this class preserve the original expression tree
/// and return a new tree containing the replaced parameter instances.
/// </para>
/// </remarks>
public static class ExpressionParameterReplacer
{
    #region Methods

    /// <summary>
    /// Replaces a single parameter expression.
    /// </summary>
    /// <param name="expression">
    /// The expression to rewrite.
    /// </param>
    /// <param name="source">
    /// The parameter to replace.
    /// </param>
    /// <param name="target">
    /// The replacement parameter.
    /// </param>
    /// <returns>
    /// A new expression with the parameter replaced.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any argument is <see langword="null"/>.
    /// </exception>
    public static Expression Replace(
        Expression expression,
        ParameterExpression source,
        ParameterExpression target)
    {
        ArgumentNullException.ThrowIfNull(expression);
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        return Replace(
            expression,
            new Dictionary<ParameterExpression, ParameterExpression>
            {
                [source] = target
            });
    }

    /// <summary>
    /// Replaces multiple parameter expressions in a single traversal.
    /// </summary>
    /// <param name="expression">
    /// The expression to rewrite.
    /// </param>
    /// <param name="parameterMap">
    /// A mapping between source parameters and their replacements.
    /// </param>
    /// <returns>
    /// A rewritten expression.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any argument is <see langword="null"/>.
    /// </exception>
    public static Expression Replace(
        Expression expression,
        IReadOnlyDictionary<ParameterExpression, ParameterExpression> parameterMap)
    {
        ArgumentNullException.ThrowIfNull(expression);
        ArgumentNullException.ThrowIfNull(parameterMap);

        if (parameterMap.Count == 0)
        {
            return expression;
        }

        var visitor = new ParameterReplaceVisitor(parameterMap);

        return visitor.Visit(expression)!;
    }

    /// <summary>
    /// Replaces the parameter of a lambda expression with another parameter.
    /// </summary>
    /// <typeparam name="T">
    /// The lambda input type.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The lambda result type.
    /// </typeparam>
    /// <param name="expression">
    /// The lambda expression to rewrite.
    /// </param>
    /// <param name="parameter">
    /// The replacement parameter.
    /// </param>
    /// <returns>
    /// A new lambda expression that uses the supplied parameter instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any argument is <see langword="null"/>.
    /// </exception>
    public static Expression<Func<T, TResult>> ReplaceParameter<T, TResult>(
        Expression<Func<T, TResult>> expression,
        ParameterExpression parameter)
    {
        ArgumentNullException.ThrowIfNull(expression);
        ArgumentNullException.ThrowIfNull(parameter);

        var body = Replace(
            expression.Body,
            expression.Parameters[0],
            parameter);

        return Expression.Lambda<Func<T, TResult>>(
            body,
            parameter);
    }

    #endregion
}