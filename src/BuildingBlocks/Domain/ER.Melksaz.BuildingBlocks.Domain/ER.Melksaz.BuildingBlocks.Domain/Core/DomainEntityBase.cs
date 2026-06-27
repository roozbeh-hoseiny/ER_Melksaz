using ER.Melksaz.BuildingBlocks.Domain.Abstractions;

namespace ER.Melksaz.BuildingBlocks.Domain.Core;

/// <summary>
/// Represents the base class for all domain entities.
/// </summary>
/// <typeparam name="TId">
/// Type of the entity identifier.
/// </typeparam>
/// <remarks>
/// This base class extends <see cref="EventEntityBase"/> to provide
/// domain event support for entities.
///
/// In Domain-Driven Design (DDD), entities are defined primarily by
/// their identity rather than their attributes.
/// </remarks>
public abstract class DomainEntityBase<TId> :
    EventEntityBase,
    IDomainEntity<TId>
    where TId : IEquatable<TId>
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    public TId Id { get; private set; }

    protected DomainEntityBase(TId id)
    {
        this.Id = id;
    }


    #region " Equals Methods "
    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>
    /// <c>true</c> if the objects are equal; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is not DomainEntityBase<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (this.Id is null || other.Id is null)
            return false;

        return this.Id.Equals(other.Id);
    }

    /// <summary>
    /// Returns the hash code for this entity.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        return this.Id?.GetHashCode() ?? 0;
    }

    public bool Equals(IDomainEntity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || this.Id.Equals(other.Id);
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    public static bool operator ==(
        DomainEntityBase<TId>? left,
        DomainEntityBase<TId>? right)
    {
        return !(left is null || right is null) && left.Equals(right);
    }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    public static bool operator !=(
        DomainEntityBase<TId>? left,
        DomainEntityBase<TId>? right)
    {
        return !Equals(left, right);
    }
    #endregion
}
