using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Services.Abstractions.Installs;

/// <summary>
/// The blender install service.
/// </summary>
public interface IBlenderInstallService
{
    /// <summary>
    /// Creates the install.
    /// </summary>
    /// <param name="install">The install.</param>
    /// <returns>A Task.</returns>
    Task CreateInstall(BlenderInstallModel install);

    /// <summary>
    /// Updates the install.
    /// </summary>
    /// <param name="install">The install.</param>
    /// <returns>A Task.</returns>
    Task UpdateInstall(BlenderInstallModel install);

    /// <summary>
    /// Deletes the install.
    /// </summary>
    /// <param name="installId">The install id.</param>
    /// <returns>A Task.</returns>
    Task DeleteInstall(Guid installId);

    /// <summary>
    /// Creates the blend file.
    /// </summary>
    /// <param name="install">The install.</param>
    /// <param name="path">The path.</param>
    /// <returns>A Task.</returns>
    void CreateBlendFile(BlenderInstallModel install, string path);
}