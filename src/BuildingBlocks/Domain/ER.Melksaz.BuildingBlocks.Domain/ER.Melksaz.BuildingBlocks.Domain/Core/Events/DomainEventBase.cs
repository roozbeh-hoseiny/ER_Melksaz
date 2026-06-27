using ER.Melksaz.BuildingBlocks.Domain.Abstractions;

namespace ER.Melksaz.BuildingBlocks.Domain.Core.Events;

public abstract record DomainEventBase(DateTimeOffset OccuredOn) : IDomainEvent
{
    public bool IsIntegrationEvent => false;
}
