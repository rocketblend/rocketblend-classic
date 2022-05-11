using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Projects;

public record ProjectDeletedEvent : EntityEvent<Project, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectDeletedEvent"/> class.
    /// </summary>
    /// <param name="project">The project.</param>
    public ProjectDeletedEvent(Project project) : base(project) { }
}