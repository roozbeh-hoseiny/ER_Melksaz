using DQ.Core.Expressions.Visitors;
using System.Linq.Expressions;

namespace DQ.Core.Expressions;

/// <summary>
/// Provides expression tree fingerprint generation.
/// </summary>
public static class ExpressionFingerprint
{
    #region Methods

    /// <summary>
    /// Creates a normalized fingerprint for an expression.
    /// </summary>
    /// <param name="expression">
    /// The expression tree.
    /// </param>
    /// <returns>
    /// A stable textual representation of the expression.
    /// </returns>
    public static string Create(Expression expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        var visitor = new ExpressionFingerprintVisitor();

        visitor.Visit(expression);

        return visitor.GetFingerprint();
    }

    #endregion
}