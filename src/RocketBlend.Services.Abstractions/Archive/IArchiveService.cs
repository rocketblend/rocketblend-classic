using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Archive;

/// <summary>
/// The archive service interface.
/// </summary>
public interface IArchiveService
{
    /// <summary>
    /// Packs the async.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="outputFile">The output file.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>A Task.</returns>
    Task PackAsync(IReadOnlyList<string> nodes, string outputFile, ArchiveType archiveType);

    /// <summary>
    /// Extracts the to new directory async.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <returns>A Task.</returns>
    Task ExtractToNewDirectoryAsync(string archivePath);

    /// <summary>
    /// Extracts the async.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A Task.</returns>
    Task ExtractAsync(string archivePath, string outputDirectory = null);

    /// <summary>
    /// Checks the if node is archive.
    /// </summary>
    /// <param name="nodePath">The node path.</param>
    /// <returns>A bool.</returns>
    bool CheckIfNodeIsArchive(string nodePath);
}