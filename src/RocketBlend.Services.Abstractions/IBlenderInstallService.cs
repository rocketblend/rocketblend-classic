using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Abstractions.Models.EventArgs;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The blender install service interface.
/// </summary>
public interface IBlenderInstallService
{
    /// <summary>
    /// Gets the blender installs.
    /// </summary>
    IReadOnlyCollection<BlenderInstallModel> Installs { get; }

    event EventHandler<BlenderInstallsListChangedEventArgs>? InstallAdded;

    event EventHandler<BlenderInstallsListChangedEventArgs>? InstallRemoved;

    /// <summary>
    /// Adds a new install.
    /// </summary>
    /// <param name="install">The install.</param>
    void AddInstall(BlenderInstallModel install);

    /// <summary>
    /// Removes a install.
    /// </summary>
    /// <param name="install">The install.</param>
    void RemoveInstall(BlenderInstallModel install);
}
