using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Projects;

public record ProjectFileCreatedEvent : EntityEvent<Project, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectFileCreatedEvent"/> class.
    /// </summary>
    /// <param name="project">The project.</param>
    public ProjectFileCreatedEvent(Project project) : base(project) { }
}