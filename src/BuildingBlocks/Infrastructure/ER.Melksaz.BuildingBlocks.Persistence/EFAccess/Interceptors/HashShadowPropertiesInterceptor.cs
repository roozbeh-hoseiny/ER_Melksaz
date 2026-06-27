using ER.Melksaz.BuildingBlocks.Application.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Interceptors;

public sealed class HashShadowPropertiesInterceptor : SaveChangesInterceptor
{
    private const string HashMappingsAnnotation = "HashMappings";

    private readonly IHasherService _hasherService;

    public HashShadowPropertiesInterceptor(
        IHasherService hasherService)
    {
        this._hasherService = hasherService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? context = eventData.Context;

        if (context is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        foreach (EntityEntry entry in context.ChangeTracker.Entries())
        {
            if (entry.State is not (
                EntityState.Added or
                EntityState.Modified))
            {
                continue;
            }

            var mappings =
                entry.Metadata
                    .FindAnnotation(HashMappingsAnnotation)
                    ?.Value as (string SourceProperty, string TargetProperty)[];

            if (mappings is null)
            {
                continue;
            }

            foreach (var mapping in mappings)
            {
                PropertyEntry sourceProperty =
                    entry.Property(mapping.SourceProperty);

                if (entry.State == EntityState.Modified &&
                    !sourceProperty.IsModified)
                {
                    continue;
                }

                object? rawValue = sourceProperty.CurrentValue;

                string? value = rawValue?.ToString();

                var hash = (value is null || string.IsNullOrWhiteSpace(value))
                    ? null
                    : this._hasherService.Hash(value);

                entry.Property(mapping.TargetProperty)
                    .CurrentValue = hash;
            }
        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}