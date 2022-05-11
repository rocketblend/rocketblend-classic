using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Domain.Entities;

/// <summary>
/// The file entry.
/// </summary>
public interface IFileEntry<TKey> : IHasKey<TKey>
{
    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Gets or sets the file location.
    /// </summary>
    public string FileLocation { get; }

    /// <summary>
    /// Gets or sets the last launched.
    /// </summary>
    //public DateTimeOffset? LastLaunched { get; set; } 
}
