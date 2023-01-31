using System.Reflection;
using CommandLine;
using RocketBlend.Launcher.Core.Options;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Services;

/// <summary>
/// The launch argument service.
/// </summary>
public class LaunchArgumentService : ILaunchArgumentService
{
    private readonly IProjectService _projectService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LaunchArgumentService"/> class.
    /// </summary>
    /// <param name="projectService">The project service.</param>
    public LaunchArgumentService(IProjectService projectService)
    {
        this._projectService = projectService;
    }

    /// <inheritdoc />
    public void HandleArguments(string[] args)
    {
        var result = Parser.Default.ParseArguments<ProjectVerbOptions>(args)
            .MapResult(
              (ProjectVerbOptions opts) => this.RunProjectVerbs(opts),
              errs => false);
    }

    /// <summary>
    /// Runs the project verbs.
    /// </summary>
    /// <param name="opts">The opts.</param>
    /// <returns>A bool.</returns>
    private bool RunProjectVerbs(ProjectVerbOptions opts)
    {
        if(!string.IsNullOrWhiteSpace(opts.OpenPath))
        {
            Guid id = Guid.Parse(opts.OpenPath);
            this._projectService.OpenProject(id);
        }
 
        return false;
    }
}
