using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Projects;

public record ProjectUpdatedEvent : EntityEvent<Project, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectUpdatedEvent"/> class.
    /// </summary>
    /// <param name="project">The project.</param>
    public ProjectUpdatedEvent(Project project) : base(project) { }
}