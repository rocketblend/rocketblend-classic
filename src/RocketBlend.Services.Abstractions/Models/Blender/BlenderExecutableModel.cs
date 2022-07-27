namespace RocketBlend.Services.Abstractions.Models.Blender;

/// <summary>
/// The blender executable model.
/// </summary>
public class BlenderExecutableModel : FileModel
{
    /// <summary>
    /// Gets or sets the build version.
    /// </summary>
    public string BuildVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the build tag.
    /// </summary>
    public string BuildTag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the build hash.
    /// </summary>
    public string BuildHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source URI.
    /// </summary>
    public Uri SourceUri { get; set; }
}
