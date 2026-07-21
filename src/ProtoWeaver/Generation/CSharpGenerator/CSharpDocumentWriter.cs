using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;

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
            var fileName = document.FileName;
            string relativePath = "";

            var docAnnotation = document.Annotations.Get<CSharpDocumentAnnotation>();
            if (docAnnotation is not null)
            {
                relativePath = docAnnotation!.RelativePath ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(docAnnotation.FileName))
                {
                    fileName = docAnnotation.FileName;
                }
            }

            var fileOutputDirectory = Path.Combine(outputDirectory, relativePath);
            Directory.CreateDirectory(fileOutputDirectory);

            var source =
                document.Builder
                    .Build()
                    .NormalizeWhitespace()
                    .ToFullString();

            File.WriteAllText(
                Path.Combine(fileOutputDirectory, fileName),
                source);
        }
    }
}