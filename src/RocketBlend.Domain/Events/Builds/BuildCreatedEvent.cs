using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Builds;

public record BuildCreatedEvent : EntityEvent<Build, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildCreatedEvent"/> class.
    /// </summary>
    /// <param name="build">The build.</param>
    public BuildCreatedEvent(Build build) : base(build) { }
}