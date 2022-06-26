using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Environment.Interfaces;
using Microsoft.Extensions.Logging;

namespace RocketBlend.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IPathService _pathService;
    private readonly IEnvironmentDirectoryService _environmentDirectoryService;
    private readonly IEnvironmentFileService _environmentFileService;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryService"/> class.
    /// </summary>
    /// <param name="pathService">The path service.</param>
    /// <param name="environmentDirectoryService">The environment directory service.</param>
    /// <param name="environmentFileService">The environment file service.</param>
    /// <param name="logger">The logger.</param>
    public DirectoryService(
        IPathService pathService,
        IEnvironmentDirectoryService environmentDirectoryService,
        IEnvironmentFileService environmentFileService,
        ILogger logger)
    {
        this._pathService = pathService;
        this._environmentDirectoryService = environmentDirectoryService;
        this._environmentFileService = environmentFileService;
        this._logger = logger;
    }

    /// <inheritdoc />
    public bool Create(string directory)
    {
        try
        {
            this._environmentDirectoryService.CreateDirectory(directory);
        }
        catch (Exception ex)
        {
            this._logger.LogError($"Failed to create directory {directory} with error {ex}");

            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public long CalculateSize(string directory) =>
        this._environmentDirectoryService
            .EnumerateFilesRecursively(directory)
            .Sum(this.GetFileSize);

    /// <inheritdoc />
    public DirectoryModel GetDirectory(string directory) => this.CreateFrom(directory);

    /// <inheritdoc />
    public DirectoryModel GetParentDirectory(string directory)
    {
        var parentDirectory = this._environmentDirectoryService.GetDirectory(directory).Parent;

        return parentDirectory is null ? null : this.CreateFrom(parentDirectory);
    }

    ///// <inheritdoc />
    //public IReadOnlyList<DirectoryModel> GetChildDirectories(string directory, ISpecification<DirectoryModel> specification = null) =>
    //    this._environmentDirectoryService
    //        .GetDirectories(directory)
    //        .Select(this.CreateFrom)
    //        .WhereNotNull()
    //        .Where(d => specification?.IsSatisfiedBy(d) ?? true)
    //        .ToArray();

    /// <inheritdoc />
    public IReadOnlyList<string> GetEmptyDirectoriesRecursively(string directory)
    {
        if (this.CheckIfEmpty(directory))
        {
            return new[] {directory};
        }

        var directories = this.GetDirectoriesRecursively(directory);

        return directories.Where(this.CheckIfEmpty).ToArray();
    }

    /// <inheritdoc />
    public bool CheckIfExists(string directory) =>
        this._environmentDirectoryService.CheckIfExists(directory);

    /// <inheritdoc />
    public IEnumerable<string> GetFilesRecursively(string directory) =>
        this._environmentDirectoryService
            .EnumerateFilesRecursively(directory);

    /// <inheritdoc />
    public IEnumerable<string> GetDirectoriesRecursively(string directory) =>
        this._environmentDirectoryService
            .EnumerateDirectoriesRecursively(directory);

    /// <summary>
    /// Gets the nodes recursively.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A list of string.</returns>
    public IEnumerable<string> GetNodesRecursively(string directory) =>
        this._environmentDirectoryService
            .EnumerateFileSystemEntriesRecursively(directory);

    /// <inheritdoc />
    public bool RemoveRecursively(string directory)
    {
        try
        {
            this._environmentDirectoryService.Delete(directory, true);
        }
        catch (Exception ex)
        {
            this._logger.LogError($"Failed to delete directory {directory} with error {ex}");

            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool Rename(string directoryPath, string newName)
    {
        var parentDirectory = this._pathService.GetParentDirectory(directoryPath);
        var newDirectoryPath = this._pathService.Combine(parentDirectory, newName);

        try
        {
            this._environmentDirectoryService.Move(directoryPath, newDirectoryPath);
        }
        catch (Exception ex)
        {
            this._logger.LogError(
                $"Failed to rename directory {directoryPath} to {newName} with error {ex}");

            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks the if empty.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A bool.</returns>
    private bool CheckIfEmpty(string directory) =>
        !this._environmentDirectoryService.EnumerateFileSystemEntriesRecursively(directory).Any();

    /// <summary>
    /// Creates the from.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <returns>A DirectoryModel.</returns>
    private DirectoryModel CreateFrom(string directory)
    {
        try
        {
            var directoryInfo = this._environmentDirectoryService.GetDirectory(directory);

            return this.CreateFrom(directoryInfo);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the file size.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A long.</returns>
    private long GetFileSize(string file) =>
        this._environmentFileService.GetFile(file).Length;

    /// <summary>
    /// Creates the from.
    /// </summary>
    /// <param name="directoryInfo">The directory info.</param>
    /// <returns>A DirectoryModel.</returns>
    private DirectoryModel CreateFrom(FileSystemInfo directoryInfo) =>
        new()
        {
            Name = directoryInfo.Name,
            FullPath = this._pathService.RightTrimPathSeparators(directoryInfo.FullName),
            LastModifiedDateTime = directoryInfo.LastWriteTime,
            LastAccessDateTime = directoryInfo.LastAccessTime,
            CreatedDateTime = directoryInfo.CreationTime
        };
}