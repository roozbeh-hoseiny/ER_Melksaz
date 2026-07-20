using System.Reflection;

namespace ProtoWeaver;

internal sealed class AssemblyCatalog
{
    private readonly Dictionary<AssemblyIdentity, string> _assemblies = [];

    public AssemblyCatalog(string assemblyPath)
    {
        var directory = Path.GetDirectoryName(assemblyPath)
            ?? throw new InvalidOperationException();

        foreach (var dll in Directory.EnumerateFiles(directory, "*.dll"))
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(dll);

                var identity = new AssemblyIdentity(assemblyName);

                this._assemblies.TryAdd(identity, dll);
            }
            catch (BadImageFormatException)
            {
                // Native DLL
            }
            catch (FileLoadException)
            {
            }
        }
    }

    public bool TryResolve(
        AssemblyName requestedAssembly,
        out string? assemblyPath)
    {
        //
        // ابتدا Full Identity
        //
        var identity = new AssemblyIdentity(requestedAssembly);

        if (this._assemblies.TryGetValue(identity, out assemblyPath))
            return true;

        //
        // اگر نسخه متفاوت بود فقط بر اساس نام جستجو کن
        //
        foreach (var pair in this._assemblies)
        {
            if (AssemblyName.ReferenceMatchesDefinition(
                    pair.Key.AssemblyName,
                    requestedAssembly))
            {
                assemblyPath = pair.Value;
                return true;
            }
        }

        assemblyPath = null;
        return false;
    }
}
internal sealed class AssemblyIdentity : IEquatable<AssemblyIdentity>
{
    public AssemblyName AssemblyName { get; }

    public AssemblyIdentity(AssemblyName assemblyName)
    {
        this.AssemblyName = assemblyName;
    }

    public bool Equals(AssemblyIdentity? other)
    {
        if (other is null)
            return false;

        return string.Equals(
                   this.AssemblyName.FullName,
                   other.AssemblyName.FullName,
                   StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
        => this.Equals(obj as AssemblyIdentity);

    public override int GetHashCode()
        => StringComparer.OrdinalIgnoreCase.GetHashCode(
            this.AssemblyName.FullName ?? string.Empty);
}