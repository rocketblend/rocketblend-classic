using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.Directory;

/// <summary>
/// The create directory operation.
/// </summary>
public class CreateDirectoryOperation : StatefulOperationWithProgressBase, IInternalOperation
{
    private readonly IDirectoryService _directoryService;
    private readonly string _directoryToCreate;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDirectoryOperation"/> class.
    /// </summary>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="directoryToCreate">The directory to create.</param>
    public CreateDirectoryOperation(
        IDirectoryService directoryService,
        string directoryToCreate)
    {
        this._directoryService = directoryService;
        this._directoryToCreate = directoryToCreate;
    }

    public Task RunAsync(CancellationToken cancellationToken)
    {
        this.State = OperationState.InProgress;
        var creationResult = this._directoryService.Create(this._directoryToCreate);
        this.State = creationResult ? OperationState.Finished : OperationState.Failed;
        this.SetFinalProgress();

        return Task.CompletedTask;
    }
}