using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The directory service.
/// </summary>
public interface IDirectoryService
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A bool.</returns>
    bool Create(string directory);

    /// <summary>
    /// Calculates the size.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A long.</returns>
    long CalculateSize(string directory);

    /// <summary>
    /// Gets the directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A DirectoryModel.</returns>
    DirectoryModel GetDirectory(string directory);

    /// <summary>
    /// Gets the parent directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A DirectoryModel.</returns>
    DirectoryModel GetParentDirectory(string directory);

    //IReadOnlyList<DirectoryModel> GetChildDirectories(string directory, ISpecification<DirectoryModel> specification = null);

    /// <summary>
    /// Gets the empty directories recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IReadOnlyList<string> GetEmptyDirectoriesRecursively(string directory);

    /// <summary>
    /// Checks the if exists.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A bool.</returns>
    bool CheckIfExists(string directory);

    /// <summary>
    /// Gets the files recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IEnumerable<string> GetFilesRecursively(string directory);

    /// <summary>
    /// Gets the directories recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IEnumerable<string> GetDirectoriesRecursively(string directory);

    /// <summary>
    /// Gets the nodes recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    IEnumerable<string> GetNodesRecursively(string directory);

    /// <summary>
    /// Removes the recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A bool.</returns>
    bool RemoveRecursively(string directory);

    /// <summary>
    /// Renames the.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>A bool.</returns>
    bool Rename(string directoryPath, string newName);
}