using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.Mapping;
using System.Reflection;

namespace ProtoWeaver.Builder;

internal static class ProtoWeaverAssemblyScanner
{
    public static void Scan(IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                RegisterAnnotationProcessors(services, type);

                RegisterGenerationSteps(services, type);

                RegisterAssignmentValueGenerators(services, type);
            }
        }
    }

    private static void RegisterAnnotationProcessors(
        IServiceCollection services,
        Type type)
    {

        RegisterByInterface(services, type, typeof(IProtoAnnotationProcessor<>));
    }

    private static void RegisterGenerationSteps(
        IServiceCollection services,
        Type type)
    {
        RegisterByInterface(services, type, typeof(IGenerationStep<>));
    }

    private static void RegisterAssignmentValueGenerators(
        IServiceCollection services,
        Type type)
    {
        foreach (var i in type.GetInterfaces())
        {
            if (i == typeof(IAssignmentValueGenerator))
                services.AddSingleton(i, type);
        }

    }

    private static void RegisterByInterface(
        IServiceCollection services,
        Type type,
        Type genericType)
    {
        foreach (var i in type.GetInterfaces())
        {
            // Register IProtoAnnotationProcessor<T>
            if (i.IsGenericType
                && i.GetGenericTypeDefinition() == genericType)
            {
                services.AddSingleton(i, type);
                continue;
            }

            // Register interfaces derived from IProtoAnnotationProcessor<T>
            if (i.GetInterfaces().Any(x =>
                    x.IsGenericType
                    && x.GetGenericTypeDefinition() == genericType))
            {
                services.AddSingleton(i, type);
            }
        }
    }
}
