using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Installs;

public record InstallCreatedEvent : EntityEvent<Install, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallCreatedEvent"/> class.
    /// </summary>
    /// <param name="install">The install.</param>
    public InstallCreatedEvent(Install install) : base(install) { }
}