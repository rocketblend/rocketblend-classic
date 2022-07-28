namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The environment directory service.
/// </summary>
public interface IEnvironmentDirectoryService
{
    /// <summary>
    /// Creates the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    void CreateDirectory(string directory);

    /// <summary>
    /// Enumerates the files recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IEnumerable<string> EnumerateFilesRecursively(string directory);

    /// <summary>
    /// Enumerates the directories recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IEnumerable<string> EnumerateDirectoriesRecursively(string directory);

    /// <summary>
    /// Enumerates the file system entries recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IEnumerable<string> EnumerateFileSystemEntriesRecursively(string directory);

    /// <summary>
    /// Gets the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A DirectoryInfo.</returns>
    DirectoryInfo GetDirectory(string directory);

    /// <summary>
    /// Gets the directories.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>An array of string.</returns>
    string[] GetDirectories(string directory);

    /// <summary>
    /// Checks the if exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A bool.</returns>
    bool CheckIfExists(string directory);

    /// <summary>
    /// Gets the current directory.
    /// </summary>
    /// <returns>A string.</returns>
    string GetCurrentDirectory();

    /// <summary>
    /// Moves the.
    /// </summary>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="destinationDirectory">The destination directory.</param>
    void Move(string sourceDirectory, string destinationDirectory);

    /// <summary>
    /// Deletes the.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="recursive">If true, recursive.</param>
    void Delete(string path, bool recursive);
}