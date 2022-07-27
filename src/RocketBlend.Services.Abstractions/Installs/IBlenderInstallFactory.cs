using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Services.Abstractions.Installs;

/// <summary>
/// The blender install factory interface.
/// </summary>
public interface IBlenderInstallFactory
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="hash">The hash.</param>
    /// <param name="path">The path.</param>
    /// <param name="downloadUrl">The download url.</param>
    /// <returns>A BlenderInstallModel.</returns>
    BlenderInstallModel Create(string name, string tag, string hash, string path, string downloadUrl);
}
