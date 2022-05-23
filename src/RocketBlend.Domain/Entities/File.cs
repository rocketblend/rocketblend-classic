

using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Domain.Entities;

/// <summary>
/// file entity abstract.
/// </summary>
public abstract class File<TKey> : AggregateRoot<TKey>, IFile
{
    /// <inheritdoc />
    public string FileName { get; protected set; } = string.Empty;

    /// <inheritdoc />
    public string FilePath { get; protected set; } = string.Empty;

    /// <inheritdoc />
    public bool IsValid => System.IO.File.Exists(this.ExecutablePath);

    /// <inheritdoc />
    public DateTimeOffset? LastLaunched => this.IsValid ? System.IO.File.GetLastWriteTimeUtc(this.ExecutablePath) : null;

    /// <inheritdoc />
    public string ExecutablePath => Path.Combine(this.FilePath, this.FileName);
}
