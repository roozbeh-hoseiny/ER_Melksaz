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
            Namespace = GetNamespace(src.Package),
            ClassDefinition = $"public static partial class {GetClassName(src.Package)}"
        };
    }
    static string GetClassName(string src) => $"{src.Split('.')[2]}Endpoints";
    static string GetNamespace(string src) => $"ER.Sanjesh.Presentation.Services.{src.Split('.')[2]}.Endpoints;";
}
