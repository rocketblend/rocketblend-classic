namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The install.
/// </summary>
public class BlenderInstallModel : FileModel
{
    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hash.
    /// </summary>
    public string Hash { get; set; } = string.Empty;
}
