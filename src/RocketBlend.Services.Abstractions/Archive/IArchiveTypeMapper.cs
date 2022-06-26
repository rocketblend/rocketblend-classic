using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Archive;

/// <summary>
/// The archive type mapper.
/// </summary>
public interface IArchiveTypeMapper
{
    /// <summary>
    /// Gets the archive type from.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A     ArchiveType? .</returns>
    ArchiveType? GetArchiveTypeFrom(string filePath);
}