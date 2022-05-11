using MediatR;
using RocketBlend.Common.Domain.Events;

namespace RocketBlend.Common.Application.Models;

/// <summary>
/// The domain event notification.
/// </summary>
public record DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventNotification"/> class.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        this.DomainEvent = domainEvent;
    }

    /// <summary>
    /// Gets the domain event.
    /// </summary>
    public TDomainEvent DomainEvent { get; }
}
