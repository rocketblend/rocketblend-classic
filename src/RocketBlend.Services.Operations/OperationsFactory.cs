using Microsoft.Extensions.Logging;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.Operations;
using RocketBlend.Services.Abstractions.Operations;
using RocketBlend.Services.Operations.Archive;
using RocketBlend.Services.Operations.Directory;
using RocketBlend.Services.Operations.Download;
using RocketBlend.Services.Operations.File;
using RocketBlend.Services.Operations.Models;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The operations factory.
/// </summary>

public class OperationsFactory : IOperationsFactory
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    private readonly IPathService _pathService;
    private readonly IFileNameGenerationService _fileNameGenerationService;
    private readonly ILogger _logger;
    private readonly IArchiveProcessorFactory _archiveProcessorFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationsFactory"/> class.
    /// </summary>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="fileService">The file service.</param>
    /// <param name="pathService">The path service.</param>
    /// <param name="fileNameGenerationService">The file name generation service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="archiveProcessorFactory">The archive processor factory.</param>
    public OperationsFactory(
        IDirectoryService directoryService,
        IFileService fileService,
        IPathService pathService,
        IFileNameGenerationService fileNameGenerationService,
        ILogger logger,
        IArchiveProcessorFactory archiveProcessorFactory)
    {
        this._directoryService = directoryService;
        this._fileService = fileService;
        this._pathService = pathService;
        this._fileNameGenerationService = fileNameGenerationService;
        this._logger = logger;
        this._archiveProcessorFactory = archiveProcessorFactory;
    }

    /// <inheritdoc />
    public IOperation CreateCopyOperation(BinaryFileSystemOperationSettings settings)
    {
        var copyOperations = this.CreateCopyOperations(settings.FilesDictionary, settings.EmptyDirectories);
        var operationGroup = CreateOperationGroup(copyOperations);

        var operations = CreateOperationsGroupsList(operationGroup);
        var operationInfo = CreateOperationInfo(OperationType.Copy, settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <inheritdoc />
    public IOperation CreateMoveOperation(BinaryFileSystemOperationSettings settings)
    {
        var copyOperations = this.CreateCopyOperations(settings.FilesDictionary, settings.EmptyDirectories);
        var copyOperationGroup = CreateOperationGroup(copyOperations);

        var deleteOldFilesOperations = this.CreateDeleteOperations(settings.InputTopLevelDirectories, settings.InputTopLevelFiles);
        var deleteOperationGroup = CreateOperationGroup(deleteOldFilesOperations);

        var operations = CreateOperationsGroupsList(copyOperationGroup, deleteOperationGroup);
        var operationInfo = CreateOperationInfo(OperationType.Move, settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <inheritdoc />
    public IOperation CreateDeleteOperation(UnaryFileSystemOperationSettings settings)
    {
        var deleteOperations = this.CreateDeleteOperations(settings.TopLevelDirectories, settings.TopLevelFiles);
        var deleteOperationGroup = CreateOperationGroup(deleteOperations);

        var operations = CreateOperationsGroupsList(deleteOperationGroup);
        var operationInfo = CreateOperationInfo(OperationType.Delete, settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <inheritdoc />
    public IOperation CreatePackOperation(PackOperationSettings settings)
    {
        var archiveWriter = this.CreateArchiveWriter(settings.ArchiveType);
        var packOperation = this.CreatePackOperation(archiveWriter, settings);
        var operationGroup = CreateOperationGroup(new[] { packOperation });
        var operations = CreateOperationsGroupsList(operationGroup);
        var operationInfo = CreateOperationInfo(settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <inheritdoc />
    public IOperation CreateExtractOperation(ExtractArchiveOperationSettings settings)
    {
        var archiveProcessor = this.CreateArchiveReader(settings.ArchiveType);
        var extractOperation = this.CreateExtractOperation(archiveProcessor, settings.InputTopLevelFile, settings.TargetDirectory);
        var operationGroup = CreateOperationGroup(new[] { extractOperation });
        var operations = CreateOperationsGroupsList(operationGroup);
        var operationInfo = CreateOperationInfo(settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <inheritdoc />
    public IOperation CreateDownloadOperation(DownloadOperationSettings settings)
    {
        var downloadOperation = this.CreateDownloadOperation(settings.SourceUri, settings.TargetDirectory);
        var operationGroup = CreateOperationGroup(new[] { downloadOperation });

        var operations = CreateOperationsGroupsList(operationGroup);
        var operationInfo = CreateOperationInfo(settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <inheritdoc />
    public IOperation CreateInstallBlenderOperation(InstallBlenderOperationSettings settings)
    {
        var downloadOperation = this.CreateDownloadOperation(settings.SourceUri, settings.DownloadDirectory);
        var downloadOperationGroup = CreateOperationGroup(new[] { downloadOperation });

        var archiveProcessor = this.CreateArchiveReader(settings.SourceArchiveType);
        var extractOperation = this.CreateExtractOperation(archiveProcessor, settings.SourcePath, settings.TargetDirectory);
        var extractOperationGroup = CreateOperationGroup(new[] { extractOperation });

        var deleteOperations = this.CreateDeleteFileOperation(settings.SourcePath);
        var deleteOperationGroup = CreateOperationGroup(new[] { deleteOperations });

        var operations = CreateOperationsGroupsList(downloadOperationGroup, extractOperationGroup, deleteOperationGroup);
        var operationInfo = CreateOperationInfo(settings);

        var compositeOperation = this.CreateCompositeOperation(operations, operationInfo);

        return this.CreateOperation(compositeOperation);
    }

    /// <summary>
    /// Creates the copy operations.
    /// </summary>
    /// <param name="filesDictionary">The files dictionary.</param>
    /// <param name="emptyDirectoriesDictionary">The empty directories dictionary.</param>
    /// <returns>An array of IInternalOperations.</returns>
    private IInternalOperation[] CreateCopyOperations(
        IReadOnlyDictionary<string, string> filesDictionary,
        IReadOnlyList<string> emptyDirectoriesDictionary)
    {
        var filesOperations = filesDictionary
            .Select(kvp => this.CreateCopyOperation(kvp.Key, kvp.Value));
        var directoriesOperations = emptyDirectoriesDictionary
            .Select(this.CreateAddDirectoryOperation);

        return filesOperations.Concat(directoriesOperations).ToArray();
    }

    /// <summary>
    /// Creates the delete operations.
    /// </summary>
    /// <param name="topLevelDirectories">The top level directories.</param>
    /// <param name="topLevelFiles">The top level files.</param>
    /// <returns>An array of IInternalOperations.</returns>
    private IInternalOperation[] CreateDeleteOperations(
        IReadOnlyList<string> topLevelDirectories,
        IReadOnlyList<string> topLevelFiles)
    {
        var fileOperations = topLevelFiles
            .Select(this.CreateDeleteFileOperation);
        var directoryOperations = topLevelDirectories
            .Select(this.CreateDeleteDirectoryOperation);

        return fileOperations.Concat(directoryOperations).ToArray();
    }

    /// <summary>
    /// Creates the copy operation.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreateCopyOperation(string source, string destination) =>
        new CopyOperation(this._directoryService, this._fileService, this._pathService, source, destination);

    /// <summary>
    /// Creates the delete file operation.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreateDeleteFileOperation(string filePath) =>
        new DeleteFileOperation(this._fileService, filePath);

    /// <summary>
    /// Creates the delete directory operation.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreateDeleteDirectoryOperation(string directoryPath) =>
        new DeleteDirectoryOperation(this._directoryService, directoryPath);

    /// <summary>
    /// Creates the add directory operation.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreateAddDirectoryOperation(string directoryPath) =>
        new CreateDirectoryOperation(this._directoryService, directoryPath);

    /// <summary>
    /// Creates the pack operation.
    /// </summary>
    /// <param name="archiveWriter">The archive writer.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreatePackOperation(IArchiveWriter archiveWriter,
        PackOperationSettings settings) =>
        new PackOperation(archiveWriter, this._directoryService, this._pathService, settings);

    /// <summary>
    /// Creates the extract operation.
    /// </summary>
    /// <param name="archiveReader">The archive reader.</param>
    /// <param name="archiveFilePath">The archive file path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreateExtractOperation(IArchiveReader archiveReader,
        string archiveFilePath, string outputDirectory) =>
        new ExtractOperation(archiveReader, this._directoryService, archiveFilePath, outputDirectory);

    /// <summary>
    /// Creates the download operation.
    /// </summary>
    /// <param name="sourceUrl">The source url.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>An IInternalOperation.</returns>
    private IInternalOperation CreateDownloadOperation(Uri sourceUrl, string outputDirectory) =>
        new DownloadOperation(sourceUrl, outputDirectory);

    /// <summary>
    /// Creates the archive reader.
    /// </summary>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An IArchiveReader.</returns>
    private IArchiveReader CreateArchiveReader(ArchiveType archiveType) =>
        this._archiveProcessorFactory.CreateReader(archiveType);

    /// <summary>
    /// Creates the archive writer.
    /// </summary>
    /// <param name="archiveType">The archive type.</param>
    /// <returns>An IArchiveWriter.</returns>
    private IArchiveWriter CreateArchiveWriter(ArchiveType archiveType) =>
        this._archiveProcessorFactory.CreateWriter(archiveType);

    /// <summary>
    /// Creates the composite operation.
    /// </summary>
    /// <param name="operations">The operations.</param>
    /// <param name="operationInfo">The operation info.</param>
    /// <returns>An ICompositeOperation.</returns>
    private ICompositeOperation CreateCompositeOperation(
        IReadOnlyList<OperationGroup> operations,
        OperationInfo operationInfo) =>
        new CompositeOperation(this._fileNameGenerationService, operations, operationInfo);

    /// <summary>
    /// Creates the operation.
    /// </summary>
    /// <param name="compositeOperation">The composite operation.</param>
    /// <returns>An IOperation.</returns>
    private IOperation CreateOperation(ICompositeOperation compositeOperation) =>
        new AsyncOperationStateMachine(compositeOperation, this._logger);

    /// <summary>
    /// Creates the operation info.
    /// </summary>
    /// <param name="operationType">The operation type.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>An OperationInfo.</returns>
    private static OperationInfo CreateOperationInfo(OperationType operationType, BinaryFileSystemOperationSettings settings) =>
        new(operationType, settings);

    /// <summary>
    /// Creates the operation info.
    /// </summary>
    /// <param name="operationType">The operation type.</param>
    /// <param name="settings">The settings.</param>
    /// <returns>An OperationInfo.</returns>
    private static OperationInfo CreateOperationInfo(OperationType operationType, UnaryFileSystemOperationSettings settings) =>
        new(operationType, settings);

    /// <summary>
    /// Creates the operation info.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An OperationInfo.</returns>
    private static OperationInfo CreateOperationInfo(PackOperationSettings settings) =>
        new(settings);

    /// <summary>
    /// Creates the operation info.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An OperationInfo.</returns>
    private static OperationInfo CreateOperationInfo(ExtractArchiveOperationSettings settings) =>
        new(settings);

    /// <summary>
    /// Creates the operation info.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An OperationInfo.</returns>
    private static OperationInfo CreateOperationInfo(DownloadOperationSettings settings) =>
        new(settings);

    /// <summary>
    /// Creates the operation info.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An OperationInfo.</returns>
    private static OperationInfo CreateOperationInfo(InstallBlenderOperationSettings settings) =>
        new(settings);

    /// <summary>
    /// Creates the operations groups list.
    /// </summary>
    /// <param name="operations">The operations.</param>
    /// <returns>A list of OperationGroups.</returns>
    private static IReadOnlyList<OperationGroup> CreateOperationsGroupsList(
        params OperationGroup[] operations) => operations;

    /// <summary>
    /// Creates the operation group.
    /// </summary>
    /// <param name="operations">The operations.</param>
    /// <returns>An OperationGroup.</returns>
    private static OperationGroup CreateOperationGroup(
        IReadOnlyList<IInternalOperation> operations) =>
        new(operations);
}