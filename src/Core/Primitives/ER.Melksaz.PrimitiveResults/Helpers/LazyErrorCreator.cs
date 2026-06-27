namespace ER.Melksaz.PrimitiveResults.Helpers;

public static class LazyErrorCreator
{
    public static Lazy<PrimitiveError> Create(string code, string message) =>
        new(() => PrimitiveError.Create(code, message));
}

