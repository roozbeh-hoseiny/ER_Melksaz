namespace ProtoWeaver.Generation.CSharpGenerator;

public sealed class CSharpDocument
{
    public required DocumentKey Key { get; init; }
    public required string FileName { get; init; }
    public required ICSharpBuilder Builder { get; init; }
    public AnnotationCollection Annotations { get; } = new();

    public T? GetBuilder<T>() where T : class, ICSharpBuilder
    {
        return this.Builder as T;
    }
}