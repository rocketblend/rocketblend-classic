﻿using RocketBlend.Common.Domain.Events;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Events.Projects;

public record ProjectCreatedEvent : EntityEvent<Project, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectCreatedEvent"/> class.
    /// </summary>
    /// <param name="project">The project.</param>
    public ProjectCreatedEvent(Project project) : base(project) { }
}