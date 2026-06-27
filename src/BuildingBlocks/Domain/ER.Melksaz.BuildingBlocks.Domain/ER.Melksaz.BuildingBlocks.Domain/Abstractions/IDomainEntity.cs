namespace ER.Melksaz.BuildingBlocks.Domain.Abstractions;

public interface IDomainEntity<TId> :
    IEventEntity, IEquatable<IDomainEntity<TId>>
    where TId : IEquatable<TId>
{
    TId Id { get; }
}

