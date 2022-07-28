using DynamicData;
using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Services.Abstractions.Installs;

/// <summary>
/// The blender install service interface.
/// </summary>
public interface IBlenderInstallStateService
{
    /// <summary>
    /// Connects the.
    /// </summary>
    /// <returns>An IObservable.</returns>
    IObservable<IChangeSet<BlenderInstallModel, Guid>> Connect();

    /// <summary>
    /// Adds the or update install.
    /// </summary>
    /// <param name="install">The install.</param>
    Task AddOrUpdateInstall(BlenderInstallModel install);

    /// <summary>
    /// Removes the install.
    /// </summary>
    /// <param name="installId">The installId.</param>
    /// <returns>A Task.</returns>
    Task RemoveInstall(Guid installId);

    /// <summary>
    /// Gets the install.
    /// </summary>
    /// <param name="installId">The install id.</param>
    /// <returns>A BlenderInstallModel?.</returns>
    BlenderInstallModel? GetInstall(Guid installId);
}