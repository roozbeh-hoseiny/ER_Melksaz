namespace ER.Melksaz.PrimitiveResults.Abstractions;

public interface IPrimitiveResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    IReadOnlyList<PrimitiveError> Errors { get; }
}
