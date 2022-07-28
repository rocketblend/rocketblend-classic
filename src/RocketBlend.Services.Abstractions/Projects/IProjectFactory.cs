using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Projects;

namespace RocketBlend.Services.Abstractions.Projects;

/// <summary>
/// The project factory.
/// </summary>
public interface IProjectFactory
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="projectName">The project name.</param>
    /// <param name="blendFiles">The blend files.</param>
    /// <returns>A ProjectModel.</returns>
    ProjectModel Create(string projectName, IList<BlendFileModel> blendFiles);
}