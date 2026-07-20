using System.Reflection;

namespace ProtoWeaver;

public sealed class AssemblyLoader
{
    public Assembly Load(string assemblyPath)
    {
        if (!File.Exists(assemblyPath))
            throw new FileNotFoundException(
                $"Assembly '{assemblyPath}' was not found.");

        var context = new ProtoAssemblyLoadContext(
            Path.GetFullPath(assemblyPath));

        return context.LoadFromAssemblyPath(
            Path.GetFullPath(assemblyPath));
    }
}