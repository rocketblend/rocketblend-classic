using System.Threading;
using System.Threading.Tasks;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.Directory;

public class DeleteDirectoryOperation : StatefulOperationWithProgressBase, IInternalOperation
{
    private readonly IDirectoryService _directoryService;
    private readonly string _directoryToRemove;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDirectoryOperation"/> class.
    /// </summary>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="directoryToRemove">The directory to remove.</param>
    public DeleteDirectoryOperation(
        IDirectoryService directoryService,
        string directoryToRemove)
    {
        this._directoryService = directoryService;
        this._directoryToRemove = directoryToRemove;
    }

    public Task RunAsync(CancellationToken cancellationToken)
    {
        this.State = OperationState.InProgress;
        var isRemoved = this._directoryService.RemoveRecursively(this._directoryToRemove);
        this.State = isRemoved ? OperationState.Finished : OperationState.Failed;
        this.SetFinalProgress();

        return Task.CompletedTask;
    }
}