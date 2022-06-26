using RocketBlend.Extensions;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.Operations;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The operations service.
/// </summary>
public class OperationsService : IOperationsService
{
    private readonly IOperationsFactory _operationsFactory;
    private readonly IDirectoryService _directoryService;
    private readonly IResourceOpeningService _resourceOpeningService;
    private readonly IFileService _fileService;
    private readonly IPathService _pathService;
    private readonly IOperationsStateService _operationsStateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationsService"/> class.
    /// </summary>
    /// <param name="operationsFactory">The operations factory.</param>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="resourceOpeningService">The resource opening service.</param>
    /// <param name="fileService">The file service.</param>
    /// <param name="pathService">The path service.</param>
    /// <param name="operationsStateService">The operations state service.</param>
    public OperationsService(
        IOperationsFactory operationsFactory,
        IDirectoryService directoryService,
        IResourceOpeningService resourceOpeningService,
        IFileService fileService,
        IPathService pathService,
        IOperationsStateService operationsStateService)
    {
        this._operationsFactory = operationsFactory;
        this._directoryService = directoryService;
        this._resourceOpeningService = resourceOpeningService;
        this._fileService = fileService;
        this._pathService = pathService;
        this._operationsStateService = operationsStateService;
    }

    /// <inheritdoc />
    public void OpenFiles(IReadOnlyList<string> files) => files.ForEach(this._resourceOpeningService.Open);

    /// <inheritdoc />
    public async Task CopyAsync(IReadOnlyList<string> nodes, string destinationDirectory)
    {
        var settings = this.GetBinaryFileSystemOperationSettings(nodes, destinationDirectory);
        var copyOperation = this._operationsFactory.CreateCopyOperation(settings);
        this._operationsStateService.AddOperation(copyOperation);

        await copyOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task MoveAsync(IReadOnlyList<string> nodes, string destinationDirectory)
    {
        var settings = this.GetBinaryFileSystemOperationSettings(nodes, destinationDirectory);
        var moveOperation = this._operationsFactory.CreateMoveOperation(settings);
        this._operationsStateService.AddOperation(moveOperation);

        await moveOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task MoveAsync(IReadOnlyDictionary<string, string> nodes)
    {
        var settings = this.GetBinaryFileSystemOperationSettings(nodes);
        var moveOperation = this._operationsFactory.CreateMoveOperation(settings);
        this._operationsStateService.AddOperation(moveOperation);

        await moveOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task PackAsync(IReadOnlyList<string> nodes, string outputFilePath, ArchiveType archiveType)
    {
        var settings = this.GetPackOperationSettings(nodes, outputFilePath, archiveType);
        var packOperation = this._operationsFactory.CreatePackOperation(settings);
        this._operationsStateService.AddOperation(packOperation);

        await packOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task ExtractAsync(string archivePath, string outputDirectory, ArchiveType archiveType)
    {
        var settings = GetExtractOperationSettings(archivePath, outputDirectory, archiveType);
        var extractOperation = this._operationsFactory.CreateExtractOperation(settings);
        this._operationsStateService.AddOperation(extractOperation);

        await extractOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task RemoveAsync(IReadOnlyList<string> nodes)
    {
        var settings = this.GetUnaryFileSystemOperationSettings(nodes);
        var deleteOperation = this._operationsFactory.CreateDeleteOperation(settings);
        this._operationsStateService.AddOperation(deleteOperation);

        await deleteOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task DownloadAsync(Uri sourceUri, string outputDirectory)
    {
        var setting = GetDownloadOperationSettings(sourceUri, outputDirectory);
        var downloadOperation = this._operationsFactory.CreateDownloadOperation(setting);
        this._operationsStateService.AddOperation(downloadOperation);

        await downloadOperation.RunAsync();
    }

    /// <inheritdoc />
    public async Task InstallBlenderAsync(Uri sourceUri, string downloadDirectory, string outputDirectory)
    {
        var settings = GetInstallBlenderOperationSettings(sourceUri, downloadDirectory, outputDirectory);
        var installBlenderOperation = this._operationsFactory.CreateInstallBlenderOperation(settings);
        this._operationsStateService.AddOperation(installBlenderOperation);

        await installBlenderOperation.RunAsync();
    }

    /// <inheritdoc />
    public bool Rename(string path, string newName)
    {
        if (this._fileService.CheckIfExists(path))
        {
            return this._fileService.Rename(path, newName);
        }

        return this._directoryService.CheckIfExists(path) && this._directoryService.Rename(path, newName);
    }

    /// <inheritdoc />
    public void CreateDirectory(string sourceDirectory, string directoryName)
    {
        var fullPath = this._pathService.Combine(sourceDirectory, directoryName);

        this._directoryService.Create(fullPath);
    }

    /// <inheritdoc />
    public void CreateFile(string sourceDirectory, string fileName)
    {
        var fullPath = this._pathService.Combine(sourceDirectory, fileName);

        this._fileService.CreateFile(fullPath);
    }

    /// <summary>
    /// Gets the binary file system operation settings.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A BinaryFileSystemOperationSettings.</returns>
    private BinaryFileSystemOperationSettings GetBinaryFileSystemOperationSettings(
        IReadOnlyList<string> nodes, string outputDirectory)
    {
        var (files, directories) = this.Split(nodes);
        var sourceDirectory = this.GetCommonRootDirectory(nodes);
        var filesInDirectories = directories.SelectMany(this._directoryService.GetFilesRecursively);
        var emptyDirectories = directories
            .SelectMany(this._directoryService.GetEmptyDirectoriesRecursively)
            .Select(d => this.GetDestinationPath(sourceDirectory, d, outputDirectory))
            .ToArray();
        var filePathsDictionary = filesInDirectories
            .Concat(files)
            .ToDictionary(
                f => f,
                f => this.GetDestinationPath(sourceDirectory, f, outputDirectory));
        var outputTopLevelFiles = files
            .Select(f => this.GetDestinationPath(sourceDirectory, f, outputDirectory))
            .ToArray();
        var outputTopLevelDirectories = directories
            .Select(f => this.GetDestinationPath(sourceDirectory, f, outputDirectory))
            .ToArray();

        return new BinaryFileSystemOperationSettings(directories, files, outputTopLevelDirectories,
            outputTopLevelFiles, filePathsDictionary, emptyDirectories, sourceDirectory, outputDirectory);
    }

    /// <summary>
    /// Gets the binary file system operation settings.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>A BinaryFileSystemOperationSettings.</returns>
    private BinaryFileSystemOperationSettings GetBinaryFileSystemOperationSettings(
        IReadOnlyDictionary<string, string> nodes)
    {
        var (files, directories) = this.Split(nodes.Keys.ToArray());
        var sourceDirectory = this.GetCommonRootDirectory(nodes.Keys.ToArray());

        var emptyDirectories = new List<string>();
        var filePathsDictionary = files.ToDictionary(f => f, f => nodes[f]);
        foreach (var directory in directories)
        {
            var filesInDirectory = this._directoryService.GetFilesRecursively(directory);
            var outputDirectory = nodes[directory];

            filesInDirectory.ForEach(f =>
                filePathsDictionary[f] = this.GetDestinationPath(directory, f, outputDirectory));
            var innerEmptyDirectories = this._directoryService
                .GetEmptyDirectoriesRecursively(directory)
                .Select(d => this.GetDestinationPath(directory, d, outputDirectory));
            emptyDirectories.AddRange(innerEmptyDirectories);
        }

        var outputTopLevelFiles = files
            .Select(f => nodes[f])
            .ToArray();
        var outputTopLevelDirectories = directories
            .Select(f => nodes[f])
            .ToArray();

        return new BinaryFileSystemOperationSettings(directories, files, outputTopLevelDirectories,
            outputTopLevelFiles, filePathsDictionary, emptyDirectories, sourceDirectory);
    }

    /// <summary>
    /// Gets the unary file system operation settings.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>An UnaryFileSystemOperationSettings.</returns>
    private UnaryFileSystemOperationSettings GetUnaryFileSystemOperationSettings(IReadOnlyList<string> nodes)
    {
        var (files, directories) = this.Split(nodes);
        var sourceDirectory = this.GetCommonRootDirectory(nodes);

        return new UnaryFileSystemOperationSettings(directories, files, sourceDirectory);
    }

    /// <summary>
    /// Gets the pack operation settings.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="outputFilePath">The output file path.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>A PackOperationSettings.</returns>
    private PackOperationSettings GetPackOperationSettings(IReadOnlyList<string> nodes, string outputFilePath, ArchiveType archiveType)
    {
        var (files, directories) = this.Split(nodes);
        var sourceDirectory = this._pathService.GetCommonRootDirectory(nodes);
        var targetDirectory = this._pathService.GetParentDirectory(outputFilePath);

        return new PackOperationSettings(directories, files, outputFilePath, sourceDirectory,
            targetDirectory, archiveType);
    }

    /// <summary>
    /// Gets the extract operation settings.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An ExtractArchiveOperationSettings.</returns>
    private static ExtractArchiveOperationSettings GetExtractOperationSettings(
        string archivePath, string outputDirectory, ArchiveType archiveType) =>
        new(archivePath, outputDirectory, archiveType);

    /// <summary>
    /// Gets the download operation settings.
    /// </summary>
    /// <param name="sourceUri">The source uri.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A DownloadOperationSettings.</returns>
    private static DownloadOperationSettings GetDownloadOperationSettings(Uri sourceUri, string outputDirectory) =>
        new(sourceUri, outputDirectory);

    /// <summary>
    /// Gets the download operation settings.
    /// </summary>
    /// <param name="sourceUri">The source uri.</param>
    /// <param name="downloadDirectory">The download directory.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>An InstallBlenderOperationSettings.</returns>
    private static InstallBlenderOperationSettings GetInstallBlenderOperationSettings(Uri sourceUri, string downloadDirectory, string outputDirectory) =>
        new(sourceUri, downloadDirectory, outputDirectory);

    /// <summary>
    /// Gets the destination path.
    /// </summary>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="sourcePath">The source path.</param>
    /// <param name="destinationDirectory">The destination directory.</param>
    /// <returns>A string.</returns>
    private string GetDestinationPath(string sourceDirectory,
        string sourcePath, string destinationDirectory)
    {
        var relativeSourcePath = this._pathService.GetRelativePath(sourceDirectory, sourcePath);

        return this._pathService.Combine(destinationDirectory, relativeSourcePath);
    }

    /// <summary>
    /// Splits the.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>A (string[] Files, string[] Directories) .</returns>
    private (string[] Files, string[] Directories) Split(IReadOnlyList<string> nodes)
    {
        var files = nodes
            .Where(this._fileService.CheckIfExists)
            .ToArray();
        var directories = nodes
            .Where(this._directoryService.CheckIfExists)
            .ToArray();

        return (files, directories);
    }

    /// <summary>
    /// Gets the common root directory.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>A string.</returns>
    private string GetCommonRootDirectory(IReadOnlyList<string> nodes) =>
        this._pathService.GetCommonRootDirectory(nodes);
}