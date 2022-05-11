using System.ComponentModel.DataAnnotations;
using RocketBlend.Common.Domain.Events;

namespace RocketBlend.Common.Domain.Entities;

/// <summary>
/// Abstract base entity class.
/// </summary>
public abstract class Entity<TKey> : IHasKey<TKey>, IAuditable, IHasDomainEvent
{
    /// <inheritdoc />
    [Key]
    public TKey Id { get; protected set; } = default!;

    /// <inheritdoc />
    public DateTimeOffset? CreatedDateTime { get; private set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedDateTime { get; private set; }

    /// <inheritdoc />
    public ICollection<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

    /// <inheritdoc />
    public void MarkCreated(DateTimeOffset timeOffset)
    {
        this.CreatedDateTime = timeOffset;
        this.UpdatedDateTime = timeOffset;
    }

    /// <inheritdoc />
    public void MarkModified(DateTimeOffset timeOffset)
    {
        this.UpdatedDateTime = timeOffset;
    }

    /// <inheritdoc />
    public void MarkEventsPublished()
    {
        this.DomainEvents.Clear();
    }

    /// <inheritdoc />
    public void AddDomainEvent(DomainEvent @event)
    {
        this.DomainEvents.Add(@event);
    }
}
