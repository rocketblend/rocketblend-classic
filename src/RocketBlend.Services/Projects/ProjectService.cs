using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Services.Projects;

/// <summary>
/// The project service.
/// </summary>
public class ProjectService : IProjectService
{
    private readonly IProjectStateService _projectStateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectService"/> class.
    /// </summary>
    /// <param name="projectStateService">The project state service.</param>
    public ProjectService(
        IProjectStateService projectStateService)
    {
        this._projectStateService = projectStateService;
    }

    /// <inheritdoc />
    public async Task CreateProject(ProjectModel project)
    {
        await this._projectStateService.AddOrUpdateProject(project);
    }

    /// <inheritdoc />
    public async Task UpdateProject(ProjectModel project)
    {
        await this._projectStateService.AddOrUpdateProject(project);
    }

    /// <inheritdoc />
    public async Task DeleteProject(Guid projectId)
    {
        await this._projectStateService.RemoveProject(projectId);
    }

    /// <inheritdoc />
    public void OpenProject(ProjectModel project)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    //public void OpenProject(ProjectModel project) => project.BlendFiles.ForEach(f => this._blendFileService.Open(f));
}