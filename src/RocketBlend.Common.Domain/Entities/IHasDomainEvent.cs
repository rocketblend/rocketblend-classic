using RocketBlend.Common.Domain.Events;

namespace RocketBlend.Common.Domain.Entities;

/// <summary>
/// The has domain event.
/// </summary>
public interface IHasDomainEvent
{
    /// <summary>
    /// Gets or sets the domain events.
    /// </summary>
    ICollection<DomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears the events.
    /// </summary>
    void MarkEventsPublished();

    /// <summary>
    /// Adds the domain event.
    /// </summary>
    /// <param name="event">The event.</param>
    void AddDomainEvent(DomainEvent @event);
}