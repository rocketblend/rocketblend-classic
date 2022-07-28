namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The build model.
/// </summary>
public class BlenderBuildModel : IHasKey<Guid>
{
    /// <inheritdoc/>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the download url.
    /// </summary>
    public string DownloadUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hash.
    /// </summary>
    public string Hash { get; set; } = string.Empty;
}