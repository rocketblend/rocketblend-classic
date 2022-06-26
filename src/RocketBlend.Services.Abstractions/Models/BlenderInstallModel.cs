namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The install.
/// </summary>
public class BlenderInstallModel : FileModel, IHasKey<Guid>
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hash.
    /// </summary>
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public string BackgroundColor { get; set; } = "#414141";

    /// <summary>
    /// Gets or sets the source URI.
    /// </summary>
    public Uri? SourceUri { get; set; }

    /// <summary>
    /// Gets the parent directory.
    /// </summary>
    public string ParentDirectory
    {
        get
        {
            var path = Path.GetDirectoryName(this.FullPath);
            var parentDir = Directory.GetParent(path ?? string.Empty);
            return parentDir is not null ? parentDir.ToString() : string.Empty;
        }
    }
}
