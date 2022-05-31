namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The environment file service.
/// </summary>
public interface IEnvironmentFileService
{
    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A FileInfo.</returns>
    FileInfo GetFile(string file);

    /// <summary>
    /// Gets the files.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>An array of string.</returns>
    string[] GetFiles(string directory);

    /// <summary>
    /// Checks the if exists.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A bool.</returns>
    bool CheckIfExists(string filePath);

    /// <summary>
    /// Moves the.
    /// </summary>
    /// <param name="oldFilePath">The old file path.</param>
    /// <param name="newFilePath">The new file path.</param>
    void Move(string oldFilePath, string newFilePath);

    /// <summary>
    /// Deletes the.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    void Delete(string filePath);

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
    /// Creates the.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    void Create(string filePath);

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