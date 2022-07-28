using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Presentation.Factories.Interfaces;

/// <summary>
/// The install view model factory interface.
/// </summary>
public interface IInstallViewModelFactory
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>An IInstallViewModel.</returns>
    public IInstallViewModel Create(BlenderInstallModel model);
}