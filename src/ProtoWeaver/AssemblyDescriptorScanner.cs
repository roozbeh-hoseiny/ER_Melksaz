using Google.Protobuf.Reflection;
using System.Reflection;

namespace ProtoWeaver;

public static class AssemblyDescriptorScanner
{
    public static IReadOnlyCollection<FileDescriptor> Scan(Assembly assembly)
    {
        var descriptors = new List<FileDescriptor>();

        Type[] types;

        try
        {
            types = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            types = ex.Types
                .Where(x => x != null)
                .ToArray()!;

            foreach (var error in ex.LoaderExceptions)
            {
                Console.WriteLine(error?.Message);
            }
        }


        foreach (var type in types)
        {
            if (!type.Name.EndsWith(
                    "Reflection",
                    StringComparison.Ordinal))
            {
                continue;
            }


            var descriptor =
                GetDescriptor(type);


            if (descriptor != null)
            {
                descriptors.Add(descriptor);
            }
        }


        return descriptors;
    }



    private static FileDescriptor? GetDescriptor(
        Type type)
    {
        var property =
            type.GetProperty(
                "Descriptor",
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);


        if (property == null)
            return null;


        if (property.PropertyType != typeof(FileDescriptor))
            return null;


        return property.GetValue(null)
            as FileDescriptor;
    }
}