using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Archive;

/// <summary>
/// The archive processor factory.
/// </summary>
public interface IArchiveProcessorFactory
{
    /// <summary>
    /// Creates the reader.
    /// </summary>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An IArchiveReader.</returns>
    IArchiveReader CreateReader(ArchiveType archiveType);

    /// <summary>
    /// Creates the writer.
    /// </summary>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An IArchiveWriter.</returns>
    IArchiveWriter CreateWriter(ArchiveType archiveType);
}