using RocketBlend.Services.Abstractions.Models.Blender;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The blend file service interface.
/// </summary>
public interface IBlendFileService
{
    /// <summary>
    /// Opens the.
    /// </summary>
    /// <param name="model">The model.</param>
    public void Open(BlendFileModel model);

    /// <summary>
    /// Renders the.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task.</returns>
    public void Render(BlendFileModel model);

    /// <summary>
    /// Renders the animation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task.</returns>
    public void RenderAnimation(BlendFileModel model);

    /// <summary>
    /// Creates the blend file.
    /// </summary>
    /// <param name="model">The model.</param>
    public void CreateBlendFile(BlendFileModel model);

    /// <summary>
    /// Deletes the blend file.
    /// </summary>
    /// <param name="model">The model.</param>
    public void DeleteBlendFile(BlendFileModel model);
}