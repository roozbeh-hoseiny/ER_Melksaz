namespace ProtoWeaver.Generation.Contracts;

public interface IDocumentWriter
{
    void Write(GenerationContext context, string outputDirectory);
}
