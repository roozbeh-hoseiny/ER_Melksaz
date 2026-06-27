using ER.Melksaz.BuildingBlocks.Domain.Abstractions;
using ER.Melksaz.BuildingBlocks.Domain.Core.Events;
using System.Collections.Concurrent;
using System.Reflection;

namespace ER.Melksaz.BuildingBlocks.Persistence;

public static class EventFactory
{
    private static readonly ConcurrentDictionary<Type, Func<object, IDomainEvent?>> _cache = new();
    private static readonly Type _referencedEntityEventInterfaceType =
        typeof(IReferencedEntityEvent<,,,>);


    public static IDomainEvent? Create(IDomainEvent domainEvent)
    {
        var type = domainEvent.GetType();

        var factory = _cache.GetOrAdd(type, t =>
        {
            var iface = t.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == _referencedEntityEventInterfaceType);

            if (iface == null) return null;

            // Get the concrete type of the event (e.g., NewRegionCreatedEvent) from the interface
            var genericArgs = iface.GetGenericArguments();
            var entityType = genericArgs[0];
            var eventType = genericArgs[3];

            // 1. Check if TEvent has a static method, e.g., "CreateFrom(TEntity entity)"
            var customFactoryMethod = eventType.GetMethod("CreateFrom",
                BindingFlags.Public | BindingFlags.Static,
                null,
                [type],
                null);

            // Explicitly define the Func type
            Func<object, IDomainEvent> factoryFunc;

            if (customFactoryMethod is not null)
            {
                factoryFunc = (object obj) =>
                {
                    return (IDomainEvent)customFactoryMethod.Invoke(null, [obj])!;
                };
            }
            else
            {
                var method = typeof(ReferencedEntityEvent<,,,>)
                    .MakeGenericType(genericArgs)
                    .GetMethod("CreateEvent", BindingFlags.Public | BindingFlags.Static);

                factoryFunc = (object obj) => (IDomainEvent)method!.Invoke(null, [obj])!;
            }

            return factoryFunc;
        });

        return factory?.Invoke(domainEvent) ?? domainEvent;
    }
}