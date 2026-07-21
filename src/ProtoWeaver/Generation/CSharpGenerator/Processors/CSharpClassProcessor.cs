using Microsoft.CodeAnalysis.CSharp;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Processors;

internal sealed class CSharpClassProcessor : IProtoServiceAnnotationProcessor
{
    public int Order => 1;

    public void Process(ProtoService src)
    {
        var annotation = new CSharpClassAnnotation()
        {
            ClassName = GetClassName(src.Package),
            Namespace = GetNamespace(src.Package)
        };
        annotation.AddKeyword(SyntaxKind.PublicKeyword);
        annotation.AddKeyword(SyntaxKind.SealedKeyword);
        src.AddAnnotation(annotation);
    }
    private static string GetClassName(string src) => $"{src.Split('.')[2]}Endpoints";
    private static string GetNamespace(string src) => $"ER.Sanjesh.Presentation.Services.{src.Split('.')[2]}.Endpoints";
}
