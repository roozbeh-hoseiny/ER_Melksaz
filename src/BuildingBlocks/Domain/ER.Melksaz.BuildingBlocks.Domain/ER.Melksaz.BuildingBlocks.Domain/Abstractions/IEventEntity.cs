namespace ER.Melksaz.BuildingBlocks.Domain.Abstractions;

public interface IEventEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
