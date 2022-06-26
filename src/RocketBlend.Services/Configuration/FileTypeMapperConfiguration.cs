using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Configuration;

/// <summary>
/// The file type mapper configuration.
/// </summary>
public class FileTypeMapperConfiguration
{
    /// <summary>
    /// Gets or sets the file type to extension dictionary.
    /// </summary>
    public Dictionary<FileContentType, string[]> FileTypeToExtensionDictionary { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeMapperConfiguration"/> class.
    /// </summary>
    public FileTypeMapperConfiguration()
    {
        this.FileTypeToExtensionDictionary = new Dictionary<FileContentType, string[]>();
    }
}