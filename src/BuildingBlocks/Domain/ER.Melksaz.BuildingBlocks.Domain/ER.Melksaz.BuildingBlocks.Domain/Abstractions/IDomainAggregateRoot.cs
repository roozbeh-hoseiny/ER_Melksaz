namespace ER.Melksaz.BuildingBlocks.Domain.Abstractions;

internal interface IDomainAggregateRoot<TId> :
    IDomainEntity<TId>
    where TId : IEquatable<TId>
{
}

