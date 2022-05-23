using MediatR;
using RocketBlend.Blender.Interfaces;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Common.Application.Models;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Events.Projects;

namespace RocketBlend.Application.EventHandlers;

/// <summary>
/// The project created event handler.
/// </summary>
public class ProjectCreatedEventHandler : INotificationHandler<DomainEventNotification<ProjectCreatedEvent>>
{
    private readonly ICrudService<Install, Guid> _installService;
    private readonly IBlenderClient _blenderClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectCreatedEventHandler"/> class.
    /// </summary>
    /// <param name="blenderClient">The blender client.</param>
    public ProjectCreatedEventHandler(ICrudService<Install, Guid> installService, IBlenderClient blenderClient)
    {
        this._installService = installService;
        this._blenderClient = blenderClient;
    }

    /// <inheritdoc />
    public async Task Handle(DomainEventNotification<ProjectCreatedEvent> notification, CancellationToken cancellationToken)
    {
        // Need to get install entity as event doesn't contain full reference.
        var install = await this._installService.GetByIdAsync(notification.DomainEvent.Entity.InstallId, true);

        if (!notification.DomainEvent.Entity.IsValid && install!.IsValid)
        {
            this._blenderClient.Executable = install.ExecutablePath;
            await this._blenderClient.CreateProject(notification.DomainEvent.Entity.ExecutablePath);
        }
    }
}
