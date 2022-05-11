using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Installs;

public record InstallDeletedEvent : EntityEvent<Install, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallDeletedEvent"/> class.
    /// </summary>
    /// <param name="install">The install.</param>
    public InstallDeletedEvent(Install install) : base(install) { }
}