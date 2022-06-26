using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.Archive;

/// <summary>
/// The extract operation.
/// </summary>
public class ExtractOperation : StatefulOperationWithProgressBase, IInternalOperation
{
    private readonly IArchiveReader _archiveReader;
    private readonly IDirectoryService _directoryService;
    private readonly string _archiveFilePath;
    private readonly string _outputDirectory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtractOperation"/> class.
    /// </summary>
    /// <param name="archiveReader">The archive reader.</param>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="archiveFilePath">The archive file path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    public ExtractOperation(
        IArchiveReader archiveReader,
        IDirectoryService directoryService,
        string archiveFilePath,
        string outputDirectory)
    {
        this._archiveReader = archiveReader;
        this._directoryService = directoryService;
        this._archiveFilePath = archiveFilePath;
        this._outputDirectory = outputDirectory;
    }

    /// <inheritdoc />
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        this.CreateOutputDirectoryIfNeeded(this._outputDirectory);

        this.State = OperationState.InProgress;
        await this._archiveReader.ExtractAsync(this._archiveFilePath, this._outputDirectory);
        this.State = OperationState.Finished;
        this.SetFinalProgress();
    }

    /// <summary>
    /// Creates the output directory if needed.
    /// </summary>
    /// <param name="outputDirectory">The output directory.</param>
    private void CreateOutputDirectoryIfNeeded(string outputDirectory)
    {
        if (!this._directoryService.CheckIfExists(outputDirectory))
        {
            this._directoryService.Create(outputDirectory);
        }
    }
}