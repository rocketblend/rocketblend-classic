using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Projects;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Main.Projects;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Presentation.Factories.Implementations;

/// <summary>
/// The project view model factory.
/// </summary>
public class ProjectViewModelFactory : IProjectViewModelFactory
{
    private readonly ISystemDialogService _systemDialogService;
    private readonly IBlenderInstallStateService _blenderInstallStateService;
    private readonly IBlenderService _blenderService;
    private readonly IProjectService _projectService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectViewModelFactory"/> class.
    /// </summary>
    /// <param name="systemDialogService">The system dialog service.</param>
    /// <param name="blenderInstallStateService">The blender install state service.</param>
    /// <param name="blenderService">The blender service.</param>
    /// <param name="projectService">The project service.</param>
    public ProjectViewModelFactory(
        ISystemDialogService systemDialogService,
        IBlenderInstallStateService blenderInstallStateService,
        IBlenderService blenderService,
        IProjectService projectService)
    {
        this._systemDialogService = systemDialogService;
        this._blenderInstallStateService = blenderInstallStateService;
        this._blenderService = blenderService;
        this._projectService = projectService;
    }

    /// <inheritdoc />
    public IProjectViewModel Create(ProjectModel model)
    {
        return new ProjectViewModel(
            this._systemDialogService,
            this._projectService,
            this._blenderService,
            this._blenderInstallStateService,
            model);
    }
}
