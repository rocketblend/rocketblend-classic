using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Installs;

public record InstallUpdatedEvent : EntityEvent<Install, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallUpdatedEvent"/> class.
    /// </summary>
    /// <param name="install">The install.</param>
    public InstallUpdatedEvent(Install install) : base(install) { }
}