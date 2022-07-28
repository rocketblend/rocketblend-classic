using Microsoft.Extensions.Logging;
using RocketBlend.Extensions;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services;

/// <summary>
/// The file service.
/// </summary>
public class FileService : IFileService
{
    private readonly IPathService _pathService;
    private readonly IEnvironmentFileService _environmentFileService;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileService"/> class.
    /// </summary>
    /// <param name="pathService">The path service.</param>
    /// <param name="environmentFileService">The environment file service.</param>
    /// <param name="logger">The logger.</param>
    public FileService(
        IPathService pathService,
        IEnvironmentFileService environmentFileService,
        ILogger logger)
    {
        this._pathService = pathService;
        this._environmentFileService = environmentFileService;
        this._logger = logger;
    }

    /// <inheritdoc />
    public IReadOnlyList<FileModel> GetFiles(IEnumerable<string> files) =>
        files.Select(this.CreateFrom).WhereNotNull().ToArray();

    /// <inheritdoc />
    public FileModel GetFile(string file) => this.CreateFrom(file);

    /// <inheritdoc />
    public bool CheckIfExists(string file) => this._environmentFileService.CheckIfExists(file);

    /// <inheritdoc />
    public bool CheckIfExtension(string file, string extension) => this._pathService.GetExtension(file) == extension;

    /// <inheritdoc />
    public async Task<bool> CopyAsync(string source, string destination, CancellationToken cancellationToken,
        bool overwrite)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (this.CheckIfExists(destination) && !overwrite)
        {
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await using var readStream = this._environmentFileService.OpenRead(source);
            await using var writeStream = this._environmentFileService.OpenWrite(destination);
            await readStream.CopyToAsync(writeStream, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            this._logger.LogInformation(
                $"Cancelled file copy {source} to {destination} (overwrite: {overwrite})");

            throw;
        }
        catch (Exception ex)
        {
            this._logger.LogError(
                $"Failed to copy file {source} to {destination} (overwrite: {overwrite}) with error {ex}");

            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool Remove(string file)
    {
        try
        {
            this._environmentFileService.Delete(file);
        }
        catch (Exception ex)
        {
            this._logger.LogError($"Failed to remove file {file} with error {ex}");

            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool Rename(string filePath, string newName)
    {
        var parentDirectory = this._pathService.GetParentDirectory(filePath);
        var newFilePath = this._pathService.Combine(parentDirectory, newName);

        try
        {
            this._environmentFileService.Move(filePath, newFilePath);
        }
        catch (Exception ex)
        {
            this._logger.LogError($"Failed to rename file {filePath} to {newName} with error {ex}");

            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public Task WriteTextAsync(string filePath, string text) =>
        this._environmentFileService.WriteTextAsync(filePath, text);

    /// <inheritdoc />
    public Task WriteBytesAsync(string filePath, byte[] bytes) =>
        this._environmentFileService.WriteBytesAsync(filePath, bytes);

    /// <inheritdoc />
    public void CreateFile(string filePath)
    {
        try
        {
            this._environmentFileService.Create(filePath);
        }
        catch (Exception ex)
        {
            this._logger.LogError($"Failed to create file {filePath} with error {ex}");
        }
    }

    /// <inheritdoc />
    public Stream OpenRead(string filePath) => this._environmentFileService.OpenRead(filePath);

    /// <inheritdoc />
    public Stream OpenWrite(string filePath) => this._environmentFileService.OpenWrite(filePath);

    /// <summary>
    /// Creates the from.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A FileModel.</returns>
    private FileModel? CreateFrom(string file)
    {
        try
        {
            var fileInfo = this._environmentFileService.GetFile(file);
            var fileModel = new FileModel
            {
                Name = fileInfo.Name,
                FullPath = this._pathService.RightTrimPathSeparators(fileInfo.FullName),
                LastModifiedDateTime = fileInfo.LastWriteTime,
                Type = GetFileType(fileInfo),
                SizeBytes = fileInfo.Length,
                Extension = this._pathService.GetExtension(fileInfo.Name),
                LastAccessDateTime = fileInfo.LastAccessTime,
                CreatedDateTime = fileInfo.CreationTime
            };

            return fileModel;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the file type.
    /// </summary>
    /// <param name="fileInfo">The file info.</param>
    /// <returns>A FileType.</returns>
    private static FileType GetFileType(FileSystemInfo fileInfo) =>
        fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint) ? FileType.Link : FileType.RegularFile;
}