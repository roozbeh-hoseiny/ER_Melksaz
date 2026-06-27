using ER.Melksaz.BuildingBlocks.Domain.Abstractions;

namespace ER.Melksaz.BuildingBlocks.Domain.Core;

public abstract class DomainAggregateRootBase<TId> :
    DomainEntityBase<TId>,
    IDomainAggregateRoot<TId>
    where TId : IEquatable<TId>
{
    protected DomainAggregateRootBase(TId id) : base(id) { }
}