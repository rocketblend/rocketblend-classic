using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The file model.
/// </summary>
public class FileModel : NodeModelBase
{
    /// <summary>
    /// Gets or sets the size bytes.
    /// </summary>
    public long SizeBytes { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public FileType Type { get; set; }

    /// <summary>
    /// Gets or sets the extension.
    /// </summary>
    public string Extension { get; set; } = string.Empty;
}