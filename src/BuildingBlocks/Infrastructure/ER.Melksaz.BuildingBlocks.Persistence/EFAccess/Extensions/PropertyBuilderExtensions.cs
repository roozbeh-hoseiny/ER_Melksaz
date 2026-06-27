using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Extensions;

public static class PropertyBuilderExtensions
{
    private const string HashMappingsAnnotation = "HashMappings";

    public static EntityTypeBuilder<TEntity> UseHashedProperty<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        string sourceProperty,
        string targetProperty)
        where TEntity : class
    {
        var existing =
            builder.Metadata.FindAnnotation(HashMappingsAnnotation)?.Value
                as (string SourceProperty, string TargetProperty)[]
            ?? [];

        var updated = existing
            .Append((sourceProperty, targetProperty))
            .ToArray();

        builder.Metadata.SetAnnotation(
            HashMappingsAnnotation,
            updated);

        return builder;
    }
}
public sealed record HashedPropertyInfo(string SourceProperty, string TargetProperty);
public static class HashedPropertyInfoSerializer
{
    public static string ToJson(this HashedPropertyInfo info)
        => JsonSerializer.Serialize(info);

    public static HashedPropertyInfo FromJson(string json)
        => JsonSerializer.Deserialize<HashedPropertyInfo>(json)!;
}