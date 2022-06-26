using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Configuration;

/// <summary>
/// The archive type mapper configuration.
/// </summary>
public class ArchiveTypeMapperConfiguration
{
    /// <summary>
    /// Gets or sets the extension to archive type dictionary.
    /// </summary>
    public Dictionary<string, ArchiveType> ExtensionToArchiveTypeDictionary { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveTypeMapperConfiguration"/> class.
    /// </summary>
    public ArchiveTypeMapperConfiguration()
    {
        this.ExtensionToArchiveTypeDictionary = new Dictionary<string, ArchiveType>();
    }
}