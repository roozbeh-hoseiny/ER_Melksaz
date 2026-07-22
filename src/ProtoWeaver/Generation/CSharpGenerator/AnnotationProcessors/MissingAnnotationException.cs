namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors;

public sealed class MissingAnnotationException : Exception
{
    public MissingAnnotationException(Type annotationType)
        : base($"The required annotation '{annotationType.Name}' was not found.")
    {
    }
}