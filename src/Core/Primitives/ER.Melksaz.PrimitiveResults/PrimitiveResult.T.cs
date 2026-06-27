using ER.Melksaz.PrimitiveResults.Abstractions;
using System.Diagnostics;
using System.Text;

namespace ER.Melksaz.PrimitiveResults;

/// <summary>
/// Represents the result of an operation that returns a value.
/// Provides success/failure state information together with the operation value or errors.
/// </summary>
/// <typeparam name="TValue">
/// Type of the value returned by the operation.
/// </typeparam>
[DebuggerDisplay("Success = {IsSuccess}, Errors = {Errors.Length}")]
public sealed partial class PrimitiveResult<TValue> : IPrimitiveResult<TValue>
{
    #region " Fields "

    /// <summary>
    /// Stores the value associated with a successful result.
    /// </summary>
    private TValue? _value { get; set; }

    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    private bool _isSuccess = false;

    /// <summary>
    /// Stores the collection of errors associated with a failed result.
    /// </summary>
    private IReadOnlyList<PrimitiveError> _errors = Array.Empty<PrimitiveError>();

    #endregion

    #region " Properties "

    /// <summary>
    /// Gets the value associated with the successful result.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to access the value of a failed result.
    /// </exception>
    public TValue Value => this.IsSuccess
       ? this._value!
       : throw new InvalidOperationException("the value of failure result can not be accessed.");

    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess => this._isSuccess;

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !this.IsSuccess;

    /// <summary>
    /// Gets all errors associated with the failed result.
    /// Returns an empty array when the result succeeds.
    /// </summary>
    public IReadOnlyList<PrimitiveError> Errors => this._errors;

    /// <summary>
    /// Gets the first error associated with the failed result.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when no errors exist.
    /// </exception>
    public PrimitiveError Error => this.Errors.Any()
        ? this.Errors[0]
        : throw new InvalidOperationException("empty error.");
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveResult{TValue}"/> class.
    /// </summary>
    /// <param name="value">
    /// Value associated with the result.
    /// </param>
    /// <param name="isSuccess">
    /// Indicates whether the operation succeeded.
    /// </param>
    /// <param name="errors">
    /// Errors associated with the result.
    /// </param>
    internal PrimitiveResult(TValue? value, bool isSuccess, PrimitiveError[] errors)
    {
        this._value = value;
        this._isSuccess = isSuccess;
        this._errors = errors;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveResult{TValue}"/> class.
    /// </summary>
    /// <param name="value">
    /// Value associated with the result.
    /// </param>
    /// <param name="isSuccess">
    /// Indicates whether the operation succeeded.
    /// </param>
    /// <param name="errors">
    /// Errors associated with the result.
    /// </param>
    internal PrimitiveResult(TValue? value, bool isSuccess, IReadOnlyList<PrimitiveError> errors)
    {
        this._value = value;
        this._isSuccess = isSuccess;
        this._errors = errors;
    }

    /// <summary>
    /// Sets a new value for the current successful result.
    /// </summary>
    /// <param name="newValue">
    /// New value to assign.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to set the value of a failed result.
    /// </exception>
    internal void SetValue(TValue newValue)
    {
        if (this.IsFailure)
            throw new InvalidOperationException("can no set value of failure result.");

        this._value = newValue;
    }

    /// <summary>
    /// Changes the current result to a failed state using the specified errors.
    /// </summary>
    /// <param name="errors">
    /// Errors associated with the failure.
    /// </param>
    internal void ChangeToFailure(PrimitiveError[] errors)
    {
        this._isSuccess = false;
        this._errors = errors;
    }

    /// <summary>
    /// Changes the current result to a failed state using a single error.
    /// </summary>
    /// <param name="error">
    /// Error associated with the failure.
    /// </param>
    internal void ChangeToFailure(PrimitiveError error) => this.ChangeToFailure([error]);

    /// <summary>
    /// Creates a failed generic result using the specified errors.
    /// </summary>
    /// <param name="errors">
    /// Errors associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure(PrimitiveError[] errors)
        => PrimitiveResult.Failure<TValue>(errors);

    /// <summary>
    /// Creates a successful generic result containing the specified value.
    /// </summary>
    /// <param name="value">
    /// Value associated with the successful result.
    /// </param>
    /// <returns>
    /// A successful <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Success(TValue value)
        => PrimitiveResult.Success<TValue>(value);

    /// <summary>
    /// Creates a non-generic result from the specified generic result.
    /// </summary>
    /// <param name="src">
    /// Source result to convert.
    /// </param>
    /// <returns>
    /// A successful non-generic result when the source succeeds;
    /// otherwise a failed result containing the source errors.
    /// </returns>
    public static PrimitiveResult From(PrimitiveResult<TValue> src) =>
        src.IsSuccess
        ? PrimitiveResult.Success()
        : PrimitiveResult.Failure(src.Errors);

    /// <summary>
    /// Returns the value of the specified result when successful;
    /// otherwise returns the default value of <typeparamref name="TValue"/>.
    /// </summary>
    /// <param name="src">
    /// Source result instance.
    /// </param>
    /// <returns>
    /// The result value when successful; otherwise default.
    /// </returns>
    public static TValue? GetValue(PrimitiveResult<TValue> src) =>
        src.IsSuccess
        ? src.Value
        : default;

    /// <summary>
    /// Converts a value into a successful <see cref="PrimitiveResult{TValue}"/>.
    /// </summary>
    /// <param name="value">
    /// Value to wrap in a successful result.
    /// </param>
    public static implicit operator PrimitiveResult<TValue>(TValue value)
        => PrimitiveResult.Success(value);

    /// <summary>
    /// Returns a string representation of the current result.
    /// </summary>
    /// <returns>
    /// A string containing the current result state and value or errors.
    /// </returns>
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        _ = stringBuilder.Append(" { ");

        if (this.PrintMembers(stringBuilder))
        {
            _ = stringBuilder.Append(' ');
        }

        _ = stringBuilder.Append('}');

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Appends the current result members to the specified string builder.
    /// </summary>
    /// <param name="builder">
    /// Target string builder instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> after members are appended.
    /// </returns>
    private bool PrintMembers(StringBuilder builder)
    {
        _ = builder.Append("IsSuccess = ");
        _ = builder.Append(this.IsSuccess.ToString());

        _ = builder.Append(", IsFailure = ");
        _ = builder.Append(this.IsFailure.ToString());

        if (this.IsSuccess)
        {
            _ = builder.Append(", Value = ");
            _ = builder.Append(this.Value);
        }
        else
        {
            _ = builder.Append(", Errors = ");
            _ = builder.Append(this.Errors);
        }

        return true;
    }
}
