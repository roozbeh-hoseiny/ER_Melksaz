using ER.Melksaz.PrimitiveResults.Abstractions;
using System.Diagnostics;

namespace ER.Melksaz.PrimitiveResults;

/// <summary>
/// Represents the result of an operation without a return value.
/// Provides success/failure state information along with associated errors.
/// Supports creation of both generic and non-generic result types.
/// </summary>
[DebuggerDisplay("Success = {IsSuccess}, Errors = {Errors.Length}")]
public sealed partial class PrimitiveResult : IPrimitiveResult
{
    private static readonly PrimitiveResult _Success = new PrimitiveResult(true, Array.Empty<PrimitiveError>());

    #region " Fields "
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    private bool _isSuccess = false;

    /// <summary>
    /// Stores the collection of errors associated with a failed operation.
    /// Empty when the operation succeeds.
    /// </summary>
    private IReadOnlyList<PrimitiveError> _errors = Array.Empty<PrimitiveError>();
    #endregion

    #region " Properties "
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool IsSuccess => this._isSuccess;

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !this.IsSuccess;

    /// <summary>
    /// Gets all errors associated with the current result.
    /// Returns an empty array when the operation succeeds.
    /// </summary>
    public IReadOnlyList<PrimitiveError> Errors => this._errors;

    /// <summary>
    /// Gets the first error associated with the current result.
    /// Throws an exception when no errors exist.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to access an error on a successful result.
    /// </exception>
    public PrimitiveError Error => this.Errors.Any()
        ? this.Errors[0]
        : throw new InvalidOperationException("empty error.");
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveResult"/> class.
    /// </summary>
    /// <param name="isSuccess">
    /// Indicates whether the operation succeeded.
    /// </param>
    /// <param name="errors">
    /// Collection of errors associated with the result.
    /// </param>
    private PrimitiveResult(bool isSuccess, PrimitiveError[] errors)
    {
        if (isSuccess && errors.Any())
            throw new ArgumentException("Success result can not have any errors.");

        if (!isSuccess && !errors.Any())
            throw new ArgumentException("Failure result must have error.");

        this._isSuccess = isSuccess;
        this._errors = errors;
    }
    private PrimitiveResult(bool isSuccess, IReadOnlyList<PrimitiveError> errors)
    {
        if (isSuccess && errors.Any())
            throw new ArgumentException("Success result can not have any errors.");

        if (!isSuccess && !errors.Any())
            throw new ArgumentException("Failure result must have error.");

        this._isSuccess = isSuccess;
        this._errors = errors.ToArray();
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

    ///<summary>
    /// Changes the current result to a failed state using the specified errors.
    /// </summary>
    /// <param name="error">
    /// Errors associated with the failure.
    /// </param>
    internal void ChangeToFailure(PrimitiveError error) => this.ChangeToFailure([error]);

    #region " Success Methods "
    /// <summary>
    /// Creates a successful non-generic result.
    /// </summary>
    /// <returns>
    /// A successful <see cref="PrimitiveResult"/>.
    /// </returns>
    public static PrimitiveResult Success() => _Success;

    /// <summary>
    /// Creates a successful generic result containing the specified value.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="value">
    /// Value associated with the successful result.
    /// </param>
    /// <returns>
    /// A successful <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Success<TValue>(TValue value)
        => new(value, true, Array.Empty<PrimitiveError>());
    #endregion

    #region " Failure Methods "
    /// <summary>
    /// Creates a failed result from another failed result.
    /// </summary>
    /// <param name="src">
    /// Source result containing failure errors.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult"/> containing the source errors.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the source result is successful.
    /// </exception>
    public static PrimitiveResult Failure(PrimitiveResult src) =>
        src.IsSuccess
            ? throw new InvalidOperationException("the error of success result can not be accessed.")
            : new(false, src.Errors);

    /// <summary>
    /// Creates a failed result using the specified errors.
    /// </summary>
    /// <param name="errors">
    /// Errors associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult"/>.
    /// </returns>
    public static PrimitiveResult Failure(PrimitiveError[] errors) => new(false, errors);

    /// <summary>
    /// Creates a failed result using the specified errors.
    /// </summary>
    /// <param name="errors">
    /// Errors associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult"/>.
    /// </returns>
    public static PrimitiveResult Failure(IReadOnlyList<PrimitiveError> errors) => new(false, errors);

    /// <summary>
    /// Creates a failed result using a single error.
    /// </summary>
    /// <param name="error">
    /// Error associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult"/>.
    /// </returns>
    public static PrimitiveResult Failure(PrimitiveError error) => new(false, [error]);

    /// <summary>
    /// Creates a failed result using an error code and message.
    /// </summary>
    /// <param name="errorCode">
    /// Unique code identifying the error.
    /// </param>
    /// <param name="errorMessage">
    /// Human-readable error message.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult"/>.
    /// </returns>
    public static PrimitiveResult Failure(string errorCode, string errorMessage)
        => new(false, [PrimitiveError.Create(errorCode, errorMessage)]);

    /// <summary>
    /// Creates a failed generic result using the specified errors.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="errors">
    /// Errors associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveError[] errors)
        => new(default, false, errors);

    /// <summary>
    /// Creates a failed generic result using the specified errors.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="errors">
    /// Errors associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure<TValue>(IReadOnlyList<PrimitiveError> errors)
        => new(default, false, errors);

    /// <summary>
    /// Creates a failed generic result using a single error.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="error">
    /// Error associated with the failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveError error)
        => new(default, false, [error]);

    /// <summary>
    /// Creates a failed generic result using an error code and message.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="errorCode">
    /// Unique code identifying the error.
    /// </param>
    /// <param name="errorMessage">
    /// Human-readable error message.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure<TValue>(string errorCode, string errorMessage)
        => new(default, false, [PrimitiveError.Create(errorCode, errorMessage)]);

    /// <summary>
    /// Creates a failed generic result from a non-generic result.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="result">
    /// Source result containing failure errors.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveResult result)
        => new(default, false, result.Errors);

    /// <summary>
    /// Creates a failed generic result from another generic result.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="result">
    /// Source result containing failure errors.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveResult<TValue> result)
        => new(default, false, result.Errors);

    /// <summary>
    /// Creates an internal failed result using an error code and message.
    /// </summary>
    /// <param name="errorCode">
    /// Unique code identifying the internal error.
    /// </param>
    /// <param name="errorMessage">
    /// Human-readable internal error message.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult"/> representing an internal error.
    /// </returns>
    public static PrimitiveResult InternalFailure(string errorCode, string errorMessage)
        => new(false, [PrimitiveError.CreateInternal(errorCode, errorMessage)]);

    /// <summary>
    /// Creates an internal failed generic result using an error code and message.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="errorCode">
    /// Unique code identifying the internal error.
    /// </param>
    /// <param name="errorMessage">
    /// Human-readable internal error message.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> InternalFailure<TValue>(string errorCode, string errorMessage)
        => new(default, false, [PrimitiveError.CreateInternal(errorCode, errorMessage)]);

    /// <summary>
    /// Creates an internal failed generic result using an existing error.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of the result value.
    /// </typeparam>
    /// <param name="error">
    /// Error associated with the internal failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="PrimitiveResult{TValue}"/>.
    /// </returns>
    public static PrimitiveResult<TValue> InternalFailure<TValue>(PrimitiveError error)
        => new(default, false, [PrimitiveError.CreateInternal(error.Code, error.Message)]);
    #endregion

    /// <summary>
    /// Creates a new result from an existing result instance.
    /// </summary>
    /// <param name="src">
    /// Source result to copy.
    /// </param>
    /// <returns>
    /// A success result when the source succeeds;
    /// otherwise a failed result containing the source errors.
    /// </returns>
    public static PrimitiveResult From(PrimitiveResult src) =>
        src.IsSuccess
        ? Success()
        : Failure(src.Errors);

    /// <summary>
    /// Represents an empty successful boolean result.
    /// </summary>
    public static readonly PrimitiveResult<bool> Empty = Success(true);
}
