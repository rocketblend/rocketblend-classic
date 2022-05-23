using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Domain.Entities;

/// <summary>
/// The file entry.
/// </summary>
public interface IFile
{
    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets or sets a value indicating whether is valid.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Gets or sets the last launched.
    /// </summary>
    public DateTimeOffset? LastLaunched { get; }

    /// <summary>
    /// Gets the executable path.
    /// </summary>
    public string ExecutablePath { get; }
}
