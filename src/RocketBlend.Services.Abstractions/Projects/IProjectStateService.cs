using DynamicData;
using RocketBlend.Services.Abstractions.Models.Projects;

namespace RocketBlend.Services.Abstractions.Projects;

/// <summary>
/// The project state service.
/// </summary>
public interface IProjectStateService
{
    /// <summary>
    /// Connects the.
    /// </summary>
    /// <returns>An IObservable.</returns>
    IObservable<IChangeSet<ProjectModel, Guid>> Connect();

    /// <summary>
    /// Adds the or update project.
    /// </summary>
    /// <param name="project">The project.</param>
    Task AddOrUpdateProject(ProjectModel project);

    /// <summary>
    /// Removes the project.
    /// </summary>
    /// <param name="projectId">The project id.</param>
    Task RemoveProject(Guid projectId);
}
