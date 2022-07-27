using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The file service.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Gets the files.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <returns>A list of FileModels.</returns>
    IReadOnlyList<FileModel> GetFiles(IEnumerable<string> files);

    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A FileModel.</returns>
    FileModel GetFile(string file);

    /// <summary>
    /// Checks the if exists.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A bool.</returns>
    bool CheckIfExists(string file);

    /// <summary>
    /// Checks the if extension.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="extension">The extension.</param>
    /// <returns>A bool.</returns>
    bool CheckIfExtension(string file, string extension);

    /// <summary>
    /// Copies the async.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="overwrite">If true, overwrite.</param>
    /// <returns>A Task.</returns>
    Task<bool> CopyAsync(string source, string destination, CancellationToken cancellationToken, bool overwrite = false);

    /// <summary>
    /// Removes the.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A bool.</returns>
    bool Remove(string file);

    /// <summary>
    /// Renames the.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>A bool.</returns>
    bool Rename(string filePath, string newName);

    /// <summary>
    /// Writes the text async.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="text">The text.</param>
    /// <returns>A Task.</returns>
    Task WriteTextAsync(string filePath, string text);

    /// <summary>
    /// Writes the bytes async.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="bytes">The bytes.</param>
    /// <returns>A Task.</returns>
    Task WriteBytesAsync(string filePath, byte[] bytes);

    /// <summary>
    /// Creates the file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    void CreateFile(string filePath);

    /// <summary>
    /// Opens the read.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A Stream.</returns>
    Stream OpenRead(string filePath);

    /// <summary>
    /// Opens the write.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A Stream.</returns>
    Stream OpenWrite(string filePath);
}
