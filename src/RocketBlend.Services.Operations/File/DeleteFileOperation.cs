using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.File;

/// <summary>
/// The delete file operation.
/// </summary>
public class DeleteFileOperation : StatefulOperationWithProgressBase, IInternalOperation
{
    private readonly IFileService _fileService;
    private readonly string _fileToRemove;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFileOperation"/> class.
    /// </summary>
    /// <param name="fileService">The file service.</param>
    /// <param name="fileToRemove">The file to remove.</param>
    public DeleteFileOperation(
        IFileService fileService,
        string fileToRemove)
    {
        this._fileService = fileService;
        this._fileToRemove = fileToRemove;
    }

    /// <inheritdoc />
    public Task RunAsync(CancellationToken cancellationToken)
    {
        this.State = OperationState.InProgress;
        var isRemoved = this._fileService.Remove(this._fileToRemove);
        this.State = isRemoved ? OperationState.Finished : OperationState.Failed;
        this.SetFinalProgress();

        return Task.CompletedTask;
    }
}