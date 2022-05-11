using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Builds;

public record BuildUpdatedEvent : EntityEvent<Build, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildUpdatedEvent"/> class.
    /// </summary>
    /// <param name="build">The build.</param>
    public BuildUpdatedEvent(Build build) : base(build) { }
}