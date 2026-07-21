namespace ProtoWeaver.Generation.CSharpGenerator;

public interface ICSharpDocumentWriter
{
    void Write(GenerationContext context, string outputDirectory);
}
