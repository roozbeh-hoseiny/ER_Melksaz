using System.Reflection;

namespace ProtoWeaver.Builder;

public sealed class ProtoWeaverOptions
{
    public Type? WriterType { get; set; }
    public List<Assembly> Assemblies { get; } = [];
    public string OutputDirectory { get; set; } = "Generated";
}
