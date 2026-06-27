namespace ER.Melksaz.PrimitiveResults;

public sealed record PrimitiveError
{
    public string Code { get; }
    public string Message { get; }
    public bool Internal { get; }

    private PrimitiveError(string code, string message, bool isInternal)
    {
        this.Code = code;
        this.Message = message;
        this.Internal = isInternal;
    }
    private static PrimitiveError CreateCore(string code, string message, bool isInternal) => new PrimitiveError(code, message, isInternal);
    public static PrimitiveError Create(string code, string message) => CreateCore(code, message, false);
    public static PrimitiveError CreateInternal(string code, string message) => CreateCore(code, message, true);
}
