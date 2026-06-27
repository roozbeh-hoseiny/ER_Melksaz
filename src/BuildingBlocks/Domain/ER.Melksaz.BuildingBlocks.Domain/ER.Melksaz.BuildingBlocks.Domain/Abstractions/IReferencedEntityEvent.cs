namespace ER.Melksaz.BuildingBlocks.Domain.Abstractions;

public interface IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>
    where TEntity : IDomainEntity<TId>
    where TId : IEquatable<TId>
    where TEvent : IDomainEvent
{
    TEntity SourceObject { get; }
}
