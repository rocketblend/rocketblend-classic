using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Services.Projects;

/// <summary>
/// The project factory.
/// </summary>
public class ProjectFactory : IProjectFactory
{
    private readonly IColorService _colorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectFactory"/> class.
    /// </summary>
    /// <param name="colorService">The color service.</param>
    public ProjectFactory(IColorService colorService)
    {
        this._colorService = colorService;
    }

    /// <inheritdoc />
    public ProjectModel Create(string projectName, IList<BlendFileModel> blendFiles)
    {
        var project = new ProjectModel()
        {
            Name = projectName,
            BackgroundColor = this._colorService.RandomColor(),
        };

        project.BlendFiles.AddRange(blendFiles);
        return project;
    }
}