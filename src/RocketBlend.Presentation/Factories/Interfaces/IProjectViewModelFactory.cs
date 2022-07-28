using RocketBlend.Presentation.Interfaces.Main.Projects;
using RocketBlend.Services.Abstractions.Models.Projects;

namespace RocketBlend.Presentation.Factories.Interfaces;

/// <summary>
/// The project view model factory.
/// </summary>
public interface IProjectViewModelFactory
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>An IProjectViewModel.</returns>
    public IProjectViewModel Create(ProjectModel model);
}