using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Services.Projects;

/// <summary>
/// The project service.
/// </summary>
public class ProjectService : IProjectService
{
    private readonly IBlenderService _blenderService;
    private readonly IProjectStateService _projectStateService;
    private readonly IBlenderInstallStateService _blenderInstallStateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectService"/> class.
    /// </summary>
    /// <param name="blenderService">The blender service.</param>
    /// <param name="projectStateService">The project state service.</param>
    /// <param name="installStateService">The install state service.</param>
    public ProjectService(
        IBlenderService blenderService,
        IProjectStateService projectStateService,
        IBlenderInstallStateService installStateService)
    {
        this._blenderService = blenderService;
        this._projectStateService = projectStateService;
        this._blenderInstallStateService = installStateService;
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
    public void OpenProject(Guid projectId)
    {
        var project = this._projectStateService.GetProject(projectId);
        if(project is null || project.InstallId == Guid.Empty || !project.BlendFiles.Any())
        {
            return;
        }

        var blendFile = project.BlendFiles.First();
        var install = this._blenderInstallStateService.GetInstall(project.InstallId);

        if (install is null)
        {
            return;
        }

        this._blenderService.OpenBlendWith(install.BlenderExecutable.FullPath, blendFile.FullPath);
    }

    /// <inheritdoc />
    //public void OpenProject(ProjectModel project) => project.BlendFiles.ForEach(f => this._blendFileService.Open(f));
}