using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;

/// <summary>
/// Provides utility methods for Entity Framework Core configuration, 
/// including assembly scanning for converters, comparers, and schema dependencies.
/// </summary>
public static class EFHelpers
{
    private static readonly Lazy<Type> _ValueConverterType = new Lazy<Type>(() => typeof(ValueConverter));
    private static readonly Lazy<Type> _ValueComparerType = new Lazy<Type>(() => typeof(ValueComparer));

    /// <summary>
    /// Scans the specified assembly for concrete implementations of <see cref="EntityTypeConfigurationDependency"/> 
    /// that match a specific database schema name.
    /// </summary>
    /// <param name="schema">The database schema name to filter by.</param>
    /// <param name="assembly">The assembly to scan for types.</param>
    /// <returns>A collection of instantiated <see cref="EntityTypeConfigurationDependency"/> objects matching the schema.</returns>
    public static IEnumerable<EntityTypeConfigurationDependency> FindEntityTypeConfigurationDependenciesOfSchema(
        string schema,
        Assembly assembly)
    {
        return (assembly.DefinedTypes
            ?.Where(t => !t.IsAbstract
                        && !t.IsGenericTypeDefinition
                        && typeof(EntityTypeConfigurationDependency).IsAssignableFrom(t))
            ?.Distinct()
            ?.Select(t => Activator.CreateInstance(t) as EntityTypeConfigurationDependency)
            ?.Where(a => a is not null && a.SchemaName.Equals(schema))
            ?.ToArray() ?? [])!;
    }

    /// <summary>
    /// Finds all non-abstract, non-interface types in the provided assemblies that inherit from <see cref="ValueConverter"/>.
    /// </summary>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>A collection of <see cref="TypeInfo"/> representing the discovered value converters.</returns>
    public static IEnumerable<TypeInfo> FindAllValueConvertersOfAssemblies(params Assembly[] assemblies)
    {
        var converterType = _ValueConverterType.Value;

        return assemblies.SelectMany(assembly =>
            assembly.DefinedTypes.Where(t =>
                t is { IsAbstract: false, IsInterface: false }
                && t.IsSubclassOf(converterType)));
    }

    /// <summary>
    /// Finds all non-abstract, non-interface types in the provided assemblies that inherit from <see cref="ValueComparer"/>.
    /// </summary>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>A collection of <see cref="TypeInfo"/> representing the discovered value comparers.</returns>
    public static IEnumerable<TypeInfo> FindAllValueComparerOfAssemblies(params Assembly[] assemblies)
    {
        var comparerType = _ValueComparerType.Value;

        return assemblies.SelectMany(assembly =>
            assembly.DefinedTypes.Where(t =>
                t is { IsAbstract: false, IsInterface: false }
                && t.IsSubclassOf(comparerType)));
    }

    /// <summary>
    /// Discovers all converters and comparers in the specified assemblies and applies them to the 
    /// <see cref="ModelConfigurationBuilder"/>. Optionally maps types to specific database column types.
    /// </summary>
    /// <param name="configurationBuilder">The EF Core model configuration builder.</param>
    /// <param name="allDbTypeAliases">An optional dictionary mapping a C# type to a database-specific type string (e.g., "jsonb").</param>
    /// <param name="assemblies">The assemblies to scan for converters and comparers.</param>
    public static void ApplyAllValueConvertersAndValueComparers(
        ModelConfigurationBuilder configurationBuilder,
        Dictionary<Type, string>? allDbTypeAliases,
        params Assembly[] assemblies)
    {
        var allValueConverters = FindAllValueConvertersOfAssemblies(assemblies).ToList();
        var allValueComparers = FindAllValueComparerOfAssemblies(assemblies).ToList();

        bool checkDbAliases = allDbTypeAliases is not null && allDbTypeAliases.Count > 0;

        foreach (var converter in allValueConverters)
        {
            var genericArgs = converter.BaseType!.GetGenericArguments();
            if (genericArgs.Length != 2) continue;

            var modelType = genericArgs.First();
            var comparer = allValueComparers.FirstOrDefault(x =>
                x.BaseType!.GetGenericArguments().First().Equals(modelType));

            if (checkDbAliases
                && allDbTypeAliases!.TryGetValue(modelType, out var dbAliasType)
                && !string.IsNullOrWhiteSpace(dbAliasType))
            {
                _ = configurationBuilder.Properties(
                    modelType,
                    builder => builder.HaveColumnType(dbAliasType).HaveConversion(converter, comparer));
            }
            else
            {
                _ = configurationBuilder.Properties(
                        modelType,
                        builder => builder.HaveConversion(converter, comparer));
            }
        }
    }

    /// <summary>
    /// Discovers and applies all value converters and matching comparers from the specified assemblies 
    /// to the <see cref="ModelConfigurationBuilder"/>.
    /// </summary>
    /// <param name="configurationBuilder">The EF Core model configuration builder.</param>
    /// <param name="assemblies">The assemblies to scan.</param>
    public static void ApplyAllValueConvertersAndValueComparers(
        ModelConfigurationBuilder configurationBuilder,
        params Assembly[] assemblies) => ApplyAllValueConvertersAndValueComparers(configurationBuilder, null, assemblies);
}

