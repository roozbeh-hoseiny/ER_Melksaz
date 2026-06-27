using ER.Melksaz.BuildingBlocks.Domain.Abstractions;

namespace ER.Melksaz.BuildingBlocks.Domain.Core.Events;

public abstract record ReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>(TEntity SourceObject) :
    IntegrationDomainEventBase(DateTimeOffset.Now),
    IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>
    where TEntity : IDomainEntity<TId>
    where TId : IEquatable<TId>
    where TEvent : class, IDomainEvent
{
    public static TEvent CreateEvent(IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent> referencedEvent)
    {
        // The cast to dynamic invokes the runtime binder which respects your 
        // implicit operator defined in TId
        TIdValue idValue = (TIdValue)(dynamic)referencedEvent.SourceObject.Id;

        return (TEvent)Activator.CreateInstance(typeof(TEvent), idValue)!;
    }
}