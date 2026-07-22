namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors;

internal static class AnnotationHelpers
{
    public static ReadOnlySpan<char> RemoveLastPart(ReadOnlySpan<char> span, char splitter = '_')
    {
        var index = span.LastIndexOf(splitter);
        return index >= 0
            ? span[..index]
            : span;
    }

    public static string GetServiceName(string packagedName) => packagedName.Split('.')[2];

    public static string GetPresentationMessageNamespace(string serviceName) =>
        $"ER.Sanjesh.Presentation.Services.{serviceName}.Contracts";
}
