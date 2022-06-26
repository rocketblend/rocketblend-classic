using DynamicData;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The blender install service interface.
/// </summary>
public interface IBlenderInstallService
{
    /// <summary>
    /// Connects the.
    /// </summary>
    /// <returns>An IObservable.</returns>
    public IObservable<IChangeSet<BlenderInstallModel, Guid>> Connect();

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
}
