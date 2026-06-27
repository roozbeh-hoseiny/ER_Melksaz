using ER.Melksaz.BuildingBlocks.Domain.Abstractions;

namespace ER.Melksaz.BuildingBlocks.Domain.Core;

/// <summary>
/// Represents a base class for entities that can produce domain events.
/// </summary>
/// <remarks>
/// In Domain-Driven Design, domain events represent significant occurrences
/// within the domain that other parts of the system may react to.
///
/// This base class provides infrastructure for managing domain events
/// raised by an entity or aggregate root.
///
/// Domain events are stored internally and can later be dispatched by the
/// application or infrastructure layer (for example via MediatR or an event bus).
///
/// Typical lifecycle:
/// 1. The entity raises a domain event via <see cref="AddDomainEvent(IDomainEvent)"/>.
/// 2. The event is stored internally in the entity.
/// 3. After persistence (e.g., after SaveChanges), the application layer
///    retrieves and publishes these events.
/// 4. The events are cleared using <see cref="ClearDomainEvents"/>.
/// </remarks>
public abstract class EventEntityBase : IEventEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the domain events that have been raised by this entity.
    /// </summary>
    /// <remarks>
    /// The returned collection is read-only to prevent external modification.
    /// Domain events should only be added through <see cref="AddDomainEvent"/>.
    /// </remarks>
    public IReadOnlyCollection<IDomainEvent> Events => this._domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="domainEvent">
    /// The domain event to add.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="domainEvent"/> is null.
    /// </exception>
    /// <remarks>
    /// This method should typically be used by the entity or aggregate root
    /// when a state change occurs that is meaningful to the domain.
    /// </remarks>
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            throw new ArgumentNullException(nameof(domainEvent));

        this._domainEvents.Add(domainEvent);
    }
    /// <summary>
    /// Removes a domain event from the entity.
    /// </summary>
    /// <param name="domainEvent">
    /// The domain event to add.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="domainEvent"/> is null.
    /// </exception>
    /// <remarks>
    /// This method should typically be used by the entity or aggregate root
    /// when a state change occurs that is meaningful to the domain.
    /// </remarks>
    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            throw new ArgumentNullException(nameof(domainEvent));

        _ = this._domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// Removes all domain events from the entity.
    /// </summary>
    /// <remarks>
    /// This method is typically called after the events have been
    /// successfully dispatched by the application or infrastructure layer.
    /// </remarks>
    public void ClearDomainEvents()
    {
        this._domainEvents.Clear();
    }


}
