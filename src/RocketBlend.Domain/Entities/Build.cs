using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Domain.Entities;

/// <summary>
/// The build.
/// </summary>
public class Build : AggregateRoot<Guid>
{
    /// <summary>
    /// EF constructor
    /// Initializes a new instance of the <see cref="Build"/> class.
    /// </summary>
    internal Build() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Build"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="name">The name.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="hash">The hash.</param>
    /// <param name="downloadUrl">The download URL.</param>
    /// <param name="fileSize">The file size.</param>
    public Build(Guid id, string name, string tag, string hash, string downloadUrl, string fileSize)
    {
        this.Id = id;
        this.Name = name;
        this.Tag = tag;
        this.Hash = hash;
        this.DownloadUrl = downloadUrl;
        this.Filesize = fileSize;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the tag.
    /// </summary>
    public string Tag { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the hash.
    /// </summary>
    public string Hash { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the download URL.
    /// </summary>
    public string DownloadUrl { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the file size.
    /// </summary>
    public string Filesize { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the installs navigation.
    /// </summary>
    public virtual ICollection<Install> Installs { get; private set; } = new List<Install>();
}
