using Google.Protobuf.Reflection;
using System.Reflection;
using System.Runtime.Loader;

namespace ProtoWeaver;

internal sealed class ProtoAssemblyLoadContext : AssemblyLoadContext
{
    private readonly AssemblyCatalog _catalog;

    public ProtoAssemblyLoadContext(
        string assemblyPath)
        : base(isCollectible: true)
    {
        this._catalog = new AssemblyCatalog(assemblyPath);
    }

    protected override Assembly? Load(
        AssemblyName assemblyName)
    {
        //
        // اگر قبلاً لود شده
        //
        var loadedAssembly = AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(x =>
                AssemblyName.ReferenceMatchesDefinition(
                    x.GetName(),
                    assemblyName));

        if (loadedAssembly is not null)
            return loadedAssembly;

        //
        // پیدا کردن DLL
        //
        if (this._catalog.TryResolve(
                assemblyName,
                out var assemblyPath))
        {
            return this.LoadFromAssemblyPath(assemblyPath!);
        }

        //
        // اجازه بده CLR ادامه بده
        //
        return null;
    }
}

public sealed class FileDescriptorDependencyWalker
{
    public IReadOnlyCollection<FileDescriptor> Collect(IEnumerable<FileDescriptor> roots)
    {
        var result = new HashSet<FileDescriptor>();

        foreach (var root in roots)
        {
            Visit(root, result);
        }

        return result;
    }


    private static void Visit(FileDescriptor descriptor, HashSet<FileDescriptor> result)
    {
        if (!result.Add(descriptor))
            return;

        foreach (var dependency in descriptor.Dependencies)
        {
            Visit(dependency, result);
        }
    }
}