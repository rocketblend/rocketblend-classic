namespace RocketBlend.Blender.Interfaces;

/// <summary>
/// The blender client interface.
/// </summary>
public interface IBlenderClient
{
    /// <summary>
    /// Gets or sets the working directory.
    /// </summary>
    public string WorkingDirectory { get; set; }

    /// <summary>
    /// Gets or sets the executable.
    /// </summary>
    public string Executable { get; set; }

    /// <summary>
    /// Opens the project.
    /// </summary>
    /// <param name="path">The path.</param>
    public void OpenProject(string path);

    /// <summary>
    /// Renders the single image.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="output">The output.</param>
    /// <param name="frame">The frame.</param>
    /// <param name="format">The format.</param>
    public void RenderSingleImage(string path, string output, int? frame = null, string? format = null);

    /// <summary>
    /// Renders the animation.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="output">The output.</param>
    /// <param name="startFrame">The start frame.</param>
    /// <param name="endframe">The end frame.</param>
    /// <param name="threads">The threads.</param>
    public void RenderAnimation(string path, string output, int? startFrame = null, int? endframe = null, int? threads = null);
}
