using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment directory service.
/// </summary>
public class EnvironmentDirectoryService : IEnvironmentDirectoryService
{
    /// <inheritdoc />
    public void CreateDirectory(string directory) =>
        Directory.CreateDirectory(directory);

    /// <inheritdoc />
    public IEnumerable<string> EnumerateFilesRecursively(string directory) =>
        Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories);

    /// <inheritdoc />
    public IEnumerable<string> EnumerateDirectoriesRecursively(string directory) =>
        Directory.EnumerateDirectories(directory, "*.*", SearchOption.AllDirectories);

    /// <inheritdoc />
    public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string directory) =>
        Directory.EnumerateFileSystemEntries(directory, "*.*", SearchOption.AllDirectories);

    /// <inheritdoc />
    public DirectoryInfo GetDirectory(string directory) => 
        new(directory);

    /// <inheritdoc />
    public string[] GetDirectories(string directory) =>
        Directory.GetDirectories(directory);

    /// <inheritdoc />
    public bool CheckIfExists(string directory) =>
        Directory.Exists(directory);

    /// <inheritdoc />
    public string GetCurrentDirectory() =>
        Directory.GetCurrentDirectory();

    /// <inheritdoc />
    public void Move(string sourceDirectory, string destinationDirectory) =>
        Directory.Move(sourceDirectory, destinationDirectory);

    /// <inheritdoc />
    public void Delete(string path, bool recursive) =>
        Directory.Delete(path, recursive);
}