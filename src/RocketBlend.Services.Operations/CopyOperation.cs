using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.Operations;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The copy operation.
/// </summary>
public class CopyOperation : StatefulOperationWithProgressBase, IInternalOperation, ISelfBlockingOperation
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    private readonly IPathService _pathService;
    private readonly string _sourceFile;
    private readonly string _destinationFile;

    private CancellationToken _cancellationToken;

    /// <summary>
    /// Gets the current blocked file.
    /// </summary>
    public (string SourceFilePath, string DestinationFilePath) CurrentBlockedFile { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CopyOperation"/> class.
    /// </summary>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="fileService">The file service.</param>
    /// <param name="pathService">The path service.</param>
    /// <param name="sourceFile">The source file.</param>
    /// <param name="destinationFile">The destination file.</param>
    public CopyOperation(
        IDirectoryService directoryService,
        IFileService fileService,
        IPathService pathService,
        string sourceFile,
        string destinationFile)
    {
        this._directoryService = directoryService;
        this._fileService = fileService;
        this._pathService = pathService;
        this._sourceFile = sourceFile;
        this._destinationFile = destinationFile;
    }

    /// <inheritdoc />
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        this._cancellationToken = cancellationToken;
        this._cancellationToken.ThrowIfCancellationRequested();

        this.CreateOutputDirectoryIfNeeded(this._destinationFile);

        if (this._fileService.CheckIfExists(this._destinationFile))
        {
            if (this._sourceFile == this._destinationFile)
            {
                this.State = OperationState.Skipped;
            }
            else
            {
                this.CurrentBlockedFile = (this._sourceFile, this._destinationFile);
                this.State = OperationState.Blocked;
            }
        }
        else
        {
            await this.CopyFileAsync(this._destinationFile);
        }
    }

    /// <inheritdoc />
    public async Task ContinueAsync(OperationContinuationOptions options)
    {
        switch (options.Mode)
        {
            case OperationContinuationMode.Skip:
                this.State = OperationState.Skipped;
                break;
            case OperationContinuationMode.Overwrite:
                await this.CopyFileAsync(this._destinationFile, true);
                break;
            case OperationContinuationMode.OverwriteIfOlder:
                var sourceFileDateTime = this.GetLastModifiedDateTime(this._sourceFile);
                var destinationFileDateTime = this.GetLastModifiedDateTime(this._destinationFile);
                if (sourceFileDateTime > destinationFileDateTime)
                {
                    await this.CopyFileAsync(this._destinationFile, true);
                }
                else
                {
                    this.State = OperationState.Skipped;
                }
                break;
            case OperationContinuationMode.Rename:
                await this.CopyFileAsync(options.NewFilePath);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(options.Mode), options.Mode, null);
        }

        this.SetFinalProgress();
    }

    /// <summary>
    /// Creates the output directory if needed.
    /// </summary>
    /// <param name="destinationFile">The destination file.</param>
    private void CreateOutputDirectoryIfNeeded(string destinationFile)
    {
        var outputDirectory = this._pathService.GetParentDirectory(destinationFile);
        if (!this._directoryService.CheckIfExists(outputDirectory))
        {
            this._directoryService.Create(outputDirectory);
        }
    }

    /// <summary>
    /// Gets the last modified date time.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A DateTime.</returns>
    private DateTime GetLastModifiedDateTime(string filePath) =>
        this._fileService.GetFile(filePath).LastModifiedDateTime;

    /// <summary>
    /// Copies the file async.
    /// </summary>
    /// <param name="destinationFile">The destination file.</param>
    /// <param name="force">If true, force.</param>
    /// <returns>A Task.</returns>
    private async Task CopyFileAsync(string destinationFile, bool force = false)
    {
        try
        {
            this._cancellationToken.ThrowIfCancellationRequested();

            this.State = OperationState.InProgress;
            var isCopied = await this._fileService.CopyAsync(this._sourceFile, destinationFile, this._cancellationToken, force);
            this.State = isCopied ? OperationState.Finished : OperationState.Failed;
            this.SetFinalProgress();
        }
        catch (TaskCanceledException)
        {
            this.State = OperationState.Cancelled;

            throw;
        }
    }
}