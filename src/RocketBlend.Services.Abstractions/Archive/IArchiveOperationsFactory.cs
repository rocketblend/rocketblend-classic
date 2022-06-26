using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Abstractions.Archive;

/// <summary>
/// The archive operations factory.
/// </summary>
public interface IArchiveOperationsFactory
{
    /// <summary>
    /// Creates the pack operation.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="outputFilePath">The output file path.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreatePackOperation(IReadOnlyList<string> nodes, string outputFilePath, ArchiveType archiveType);

    /// <summary>
    /// Creates the extract operation.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateExtractOperation(string archivePath, string outputDirectory, ArchiveType archiveType);
}