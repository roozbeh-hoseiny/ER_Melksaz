namespace ER.Melksaz.BuildingBlocks.Domain.Abstractions;

public interface IDomainEvent
{
    bool IsIntegrationEvent { get; }
    DateTimeOffset OccuredOn { get; }
}
