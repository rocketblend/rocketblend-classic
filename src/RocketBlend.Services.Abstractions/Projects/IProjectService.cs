using RocketBlend.Services.Abstractions.Models.Projects;

namespace RocketBlend.Services.Abstractions.Projects;

/// <summary>
/// The project service.
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Creates the project.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns>A Task.</returns>
    Task CreateProject(ProjectModel project);

    /// <summary>
    /// Updates the project.
    /// </summary>
    /// <param name="project">The project.</param>
    Task UpdateProject(ProjectModel project);

    /// <summary>
    /// Deletes the project.
    /// </summary>
    /// <param name="projectId">The project id.</param>
    Task DeleteProject(Guid projectId);

    /// <summary>
    /// Opens the project.
    /// </summary>
    /// <param name="project">The project.</param>
    void OpenProject(ProjectModel project);
}
