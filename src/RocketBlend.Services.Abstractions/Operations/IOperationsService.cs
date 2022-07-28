using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The operations service interface.
/// </summary>
public interface IOperationsService
{
    /// <summary>
    /// Opens the files.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    void OpenFiles(IReadOnlyList<string> nodes);

    /// <summary>
    /// Copies the async.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="destinationDirectory">The destination directory.</param>
    /// <returns>A Task.</returns>
    Task CopyAsync(IReadOnlyList<string> nodes, string destinationDirectory);

    /// <summary>
    /// Moves the async.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="destinationDirectory">The destination directory.</param>
    /// <returns>A Task.</returns>
    Task MoveAsync(IReadOnlyList<string> nodes, string destinationDirectory);

    /// <summary>
    /// Moves the async.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>A Task.</returns>
    Task MoveAsync(IReadOnlyDictionary<string, string> nodes);

    /// <summary>
    /// Packs the async.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="outputFilePath">The output file path.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>A Task.</returns>
    Task PackAsync(IReadOnlyList<string> nodes, string outputFilePath, ArchiveType archiveType);

    /// <summary>
    /// Extracts the async.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>A Task.</returns>
    Task ExtractAsync(string archivePath, string outputDirectory, ArchiveType archiveType);

    /// <summary>
    /// Downloads the async.
    /// </summary>
    /// <param name="sourceUri">The source uri.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A Task.</returns>
    Task DownloadAsync(Uri sourceUri, string outputDirectory);

    /// <summary>
    /// Installs the blender async.
    /// </summary>
    /// <param name="sourceUri">The source uri.</param>
    /// <param name="downloadDirectory">The download directory.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A Task.</returns>
    Task InstallBlenderAsync(Uri sourceUri, string downloadDirectory, string outputDirectory);

    /// <summary>
    /// Creates the directory.
    /// </summary>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="directoryName">The directory name.</param>
    void CreateDirectory(string sourceDirectory, string directoryName);

    /// <summary>
    /// Creates the file.
    /// </summary>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="fileName">The file name.</param>
    void CreateFile(string sourceDirectory, string fileName);

    /// <summary>
    /// Removes the async.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>A Task.</returns>
    Task RemoveAsync(IReadOnlyList<string> nodes);

    /// <summary>
    /// Renames the.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>A bool.</returns>
    bool Rename(string path, string newName);
}