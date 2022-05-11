using RocketBlend.Application.Queries.Projects;

namespace RocketBlend.Core.ViewModels.Projects;

/// <summary>
/// The project view model.
/// </summary>
public class ProjectViewModel : ViewModelBase
{
    private readonly ProjectDto _project;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectViewModel"/> class.
    /// </summary>
    /// <param name="project">The project.</param>
    public ProjectViewModel(ProjectDto project)
    {
        this._project = project;
    }

    /// <summary>
    /// Gets the id.
    /// </summary>
    public string Id => this._project.Id.ToString();

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name => this._project.Name;

    /// <summary>
    /// Gets the path.
    /// </summary>
    public string Path => this._project.Path;

    /// <summary>
    /// Gets the install executable.
    /// </summary>
    public string InstallExecutable => System.IO.Path.Combine(this._project.Install.FileLocation, this._project.Install.FileName);

    /// <summary>
    /// Gets the launch arguments.
    /// </summary>
    public string LaunchArguments => this._project.Install.LaunchArgs;
}
