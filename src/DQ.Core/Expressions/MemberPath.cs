namespace DQ.Core.Expressions;

/// <summary>
/// Represents a strongly typed member access path.
/// </summary>
/// <remarks>
/// A member path describes a nested property access chain.
///
/// Example:
///
/// <code>
/// Customer.Address.City
/// </code>
///
/// is represented as:
///
/// <code>
/// Address.City
/// </code>
///
/// This type is used by dynamic projection and expression analysis.
/// </remarks>
public sealed class MemberPath
{
    #region Fields

    private readonly IReadOnlyList<string> _members;

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="MemberPath"/> class.
    /// </summary>
    /// <param name="members">
    /// The member names composing the path.
    /// </param>
    public MemberPath(IEnumerable<string> members)
    {
        ArgumentNullException.ThrowIfNull(members);

        this._members = members.ToArray();
    }

    #endregion


    #region Properties

    /// <summary>
    /// Gets the members composing this path.
    /// </summary>
    public IReadOnlyList<string> Members => this._members;


    /// <summary>
    /// Gets the full path representation.
    /// </summary>
    public string Path => string.Join(".", this._members);

    #endregion


    #region Methods

    /// <inheritdoc />
    public override string ToString()
    {
        return this.Path;
    }

    #endregion
}