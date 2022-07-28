using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment directory service windows decorator.
/// </summary>
public class EnvironmentDirectoryServiceWindowsDecorator : IEnvironmentDirectoryService
{
    private readonly IEnvironmentDirectoryService _environmentDirectoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentDirectoryServiceWindowsDecorator"/> class.
    /// </summary>
    /// <param name="environmentDirectoryService">The environment directory service.</param>
    public EnvironmentDirectoryServiceWindowsDecorator(IEnvironmentDirectoryService environmentDirectoryService)
    {
        this._environmentDirectoryService = environmentDirectoryService;
    }

    /// <inheritdoc />
    public void CreateDirectory(string directory) => this._environmentDirectoryService.CreateDirectory(directory);

    /// <inheritdoc />
    public IEnumerable<string> EnumerateFilesRecursively(string directory) =>
        this._environmentDirectoryService.EnumerateFilesRecursively(PreprocessPath(directory));

    /// <inheritdoc />
    public IEnumerable<string> EnumerateDirectoriesRecursively(string directory) =>
        this._environmentDirectoryService.EnumerateDirectoriesRecursively(PreprocessPath(directory));

    /// <inheritdoc />
    public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string directory) =>
        this._environmentDirectoryService.EnumerateFileSystemEntriesRecursively(PreprocessPath(directory));

    /// <inheritdoc />
    public DirectoryInfo GetDirectory(string directory) =>
        this._environmentDirectoryService.GetDirectory(PreprocessPath(directory));

    /// <inheritdoc />
    public string[] GetDirectories(string directory) =>
        this._environmentDirectoryService.GetDirectories(PreprocessPath(directory));

    /// <inheritdoc />
    public bool CheckIfExists(string directory) =>
        this._environmentDirectoryService.CheckIfExists(PreprocessPath(directory));

    /// <inheritdoc />
    public string GetCurrentDirectory() => this._environmentDirectoryService.GetCurrentDirectory();

    /// <inheritdoc />
    public void Move(string sourceDirectory, string destinationDirectory) =>
        this._environmentDirectoryService.Move(sourceDirectory, destinationDirectory);

    /// <inheritdoc />
    public void Delete(string path, bool recursive) => this._environmentDirectoryService.Delete(path, recursive);

    /// <inheritdoc />
    private static string PreprocessPath(string directory) =>
        directory.EndsWith("\\") ? directory : directory + "\\";
}