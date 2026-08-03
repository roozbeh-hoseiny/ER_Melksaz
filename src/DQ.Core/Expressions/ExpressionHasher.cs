using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace DQ.Core.Expressions;

/// <summary>
/// Provides hashing utilities for expression trees.
/// </summary>
/// <remarks>
/// Hashes are generated from expression fingerprints.
/// They are suitable for dictionary keys and cache identifiers.
/// </remarks>
public static class ExpressionHasher
{
    #region Methods

    /// <summary>
    /// Creates a SHA256 hash for an expression.
    /// </summary>
    /// <param name="expression">
    /// The expression tree.
    /// </param>
    /// <returns>
    /// A hexadecimal hash value.
    /// </returns>
    public static string Hash(
        Expression expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        var fingerprint = ExpressionFingerprint.Create(expression);

        var bytes = Encoding.UTF8.GetBytes(fingerprint);

        var hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }

    #endregion
}