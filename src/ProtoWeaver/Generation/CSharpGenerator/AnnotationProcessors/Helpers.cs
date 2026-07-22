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

    public static string GetServiceName(ReadOnlySpan<char> packageName)
    {
        for (int i = 0; i < 2; i++)
        {
            var index = packageName.IndexOf('.');
            if (index < 0)
                return string.Empty;

            packageName = packageName[(index + 1)..];
        }

        var end = packageName.IndexOf('.');

        return end >= 0
            ? packageName[..end].ToString()
            : packageName.ToString();
    }

    public static string GetPresentationMessageNamespace(string serviceName) =>
        $"ER.Sanjesh.Presentation.Services.{serviceName}.Contracts";
}
