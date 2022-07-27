using RocketBlend.Services.Abstractions.Models.Blender;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The blender service.
/// </summary>
public interface IBlenderService
{
    /// <summary>
    /// Opens the.
    /// </summary>
    /// <param name="executable">The executable.</param>
    /// <param name="args">The args.</param>
    public void Open(string executable, string args = "");

    /// <summary>
    /// Opens the blend with.
    /// </summary>
    /// <param name="executable">The executable.</param>
    /// <param name="blendFile">The blend file.</param>
    /// <param name="args">The args.</param>
    public void OpenBlendWith(string executable, string blendFile, string args = "");

    /// <summary>
    /// Renders the with.
    /// </summary>
    /// <param name="executable">The executable.</param>
    /// <param name="blendFile">The blend file.</param>
    /// <param name="args">The args.</param>
    public void RenderWith(string executable, string blendFile, string args = "");

    /// <summary>
    /// Renders the animation with.
    /// </summary>
    /// <param name="executable">The executable.</param>
    /// <param name="blendFile">The blend file.</param>
    /// <param name="args">The args.</param>
    public void RenderAnimationWith(string executable, string blendFile, string args = "");

    /// <summary>
    /// Creates the blend file with.
    /// </summary>
    /// <param name="executable">The executable.</param>
    /// <param name="path">The path.</param>
    public void CreateBlendFileWith(string executable, string path);

    /// <summary>
    /// Installs the.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="sourceUri">The source uri.</param>
    public void Install(string path, Uri sourceUri);
}
