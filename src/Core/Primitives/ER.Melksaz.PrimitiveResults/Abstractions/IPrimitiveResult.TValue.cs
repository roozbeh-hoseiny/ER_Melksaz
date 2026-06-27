namespace ER.Melksaz.PrimitiveResults.Abstractions;

public interface IPrimitiveResult<TValue> : IPrimitiveResult
{
    TValue Value { get; }

}