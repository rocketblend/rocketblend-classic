using MediatR;
using Microsoft.Extensions.Logging;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Common.Application.Models;
using RocketBlend.Common.Domain.Events;

namespace RocketBlend.Common.Infrastructure.Event;

/// <summary>
/// The domain event service.
/// </summary>
public class DomainEventService : IDomainEventService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
    {
        this._logger = logger;
        this._mediator = mediator;
    }

    /// <inheritdoc />
    public async Task Publish(DomainEvent domainEvent)
    {
        this._logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
        await this._mediator.Publish(this.GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    /// <summary>
    /// Gets the notification corresponding to domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    /// <returns>An INotification.</returns>
    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}