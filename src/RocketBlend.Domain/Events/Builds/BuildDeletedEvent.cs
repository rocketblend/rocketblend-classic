using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Builds;

public record BuildDeletedEvent : EntityEvent<Build, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildDeletedEvent"/> class.
    /// </summary>
    /// <param name="build">The build.</param>
    public BuildDeletedEvent(Build build) : base(build) { }
}