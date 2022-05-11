using RocketBlend.Common.Domain.Events;

namespace RocketBlend.Common.Application.Interfaces;

/// <summary>
/// The domain event service.
/// </summary>
public interface IDomainEventService
{
    /// <summary>
    /// Publishes the.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    /// <returns>A Task.</returns>
    Task Publish(DomainEvent domainEvent);
}