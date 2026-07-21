using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ProtoWeaver.Generation.CSharpGenerator;

public sealed class CSharpDocumentWriter : ICSharpDocumentWriter
{
    public void Write(GenerationContext context, string outputDirectory)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(outputDirectory);

        Directory.CreateDirectory(outputDirectory);

        foreach (var document in context.Documents)
        {
            var source =
                document.Builder
                    .Build()
                    .NormalizeWhitespace()
                    .ToFullString();

            File.WriteAllText(
                Path.Combine(outputDirectory, document.FileName),
                source);
        }
    }
}