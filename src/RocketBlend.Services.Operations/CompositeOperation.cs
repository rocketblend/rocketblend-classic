using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RocketBlend.Extensions;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Exceptions;
using RocketBlend.Services.Abstractions.Extensions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.EventArgs;
using RocketBlend.Services.Abstractions.Models.Operations;
using RocketBlend.Services.Abstractions.Operations;
using RocketBlend.Services.Operations.Models;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The composite operation.
/// </summary>
public class CompositeOperation : OperationWithProgressBase, ICompositeOperation
{
    private readonly IFileNameGenerationService _fileNameGenerationService;
    private readonly IReadOnlyList<OperationGroup> _groupedOperationsToExecute;

    private readonly ISubject<bool> _blockedChangedSubject = new Subject<bool>();

    private readonly IDictionary<string, ISelfBlockingOperation> _blockedOperationsDictionary;
    private readonly ConcurrentQueue<(string SourceFilePath, string DestinationFilePath)> _blockedFilesQueue;
    private readonly object _locker;

    private int _finishedOperationsCount;
    private int _currentOperationsGroupIndex;
    private int _operationsGroupsCount;
    private int _totalOperationsCount;

    private IReadOnlyList<IInternalOperation>? _currentOperationsGroup;

    private CancellationTokenSource _cancellationTokenSource = new();
    private TaskCompletionSource<bool> _taskCompletionSource = new();
    private OperationContinuationMode? _continuationMode = new();

    /// <inheritdoc />
    public IObservable<bool> Blocked => this._blockedChangedSubject.AsObservable();

    /// <summary>
    /// Gets the info.
    /// </summary>
    public OperationInfo Info { get; }

    /// <summary>
    /// Gets the current blocked file.
    /// </summary>
    public (string SourceFilePath, string DestinationFilePath) CurrentBlockedFile =>
        this._blockedFilesQueue.TryPeek(out var blockedFile) ? blockedFile : default;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeOperation"/> class.
    /// </summary>
    /// <param name="fileNameGenerationService">The file name generation service.</param>
    /// <param name="groupedOperationsToExecute">The grouped operations to execute.</param>
    /// <param name="operationInfo">The operation info.</param>
    public CompositeOperation(
        IFileNameGenerationService fileNameGenerationService,
        IReadOnlyList<OperationGroup> groupedOperationsToExecute,
        OperationInfo operationInfo)
    {
        this._fileNameGenerationService = fileNameGenerationService;
        this._groupedOperationsToExecute = groupedOperationsToExecute;

        this.Info = operationInfo;

        this._blockedOperationsDictionary = new ConcurrentDictionary<string, ISelfBlockingOperation>();
        this._blockedFilesQueue = new ConcurrentQueue<(string SourceFilePath, string DestinationFilePath)>();
        this._locker = new object();
    }

    /// <inheritdoc />
    public async Task RunAsync()
    {
        var operations = this._groupedOperationsToExecute.Select(g => g.Operations).ToArray();

        await this.ExecuteOperationsAsync(operations);
    }

    /// <inheritdoc />
    public async Task ContinueAsync(OperationContinuationOptions options)
    {
        if (options.ApplyToAll)
        {
            this._continuationMode = options.Mode;
        }

        this._blockedFilesQueue.TryDequeue(out _);

        var operation = this._blockedOperationsDictionary[options.FilePath];
        this._blockedOperationsDictionary.Remove(options.FilePath);

        await operation.ContinueAsync(options);
        await this.ProcessNextBlockedTaskAsync();
    }

    /// <inheritdoc />
    public Task PauseAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UnpauseAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task CancelAsync()
    {
        this._cancellationTokenSource.Cancel();

        while (this._blockedFilesQueue.Any())
        {
            await this.ProcessNextBlockedTaskAsync();
        }

        await this._taskCompletionSource.Task;
    }

    /// <summary>
    /// Executes the operations async.
    /// </summary>
    /// <param name="groupedOperationsToExecute">The grouped operations to execute.</param>
    /// <returns>A Task.</returns>
    private async Task ExecuteOperationsAsync(
        IReadOnlyList<IReadOnlyList<IInternalOperation>> groupedOperationsToExecute)
    {
        this._cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = this._cancellationTokenSource.Token;

        this._totalOperationsCount = groupedOperationsToExecute.Sum(g => g.Count);
        this._operationsGroupsCount = groupedOperationsToExecute.Count;
        this._currentOperationsGroupIndex = 0;

        foreach (var operationsGroup in groupedOperationsToExecute)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!operationsGroup.Any())
            {
                continue;
            }

            this._taskCompletionSource = new TaskCompletionSource<bool>();

            this._finishedOperationsCount = 0;
            this._currentOperationsGroup = operationsGroup;

            for (var i = 0; i < operationsGroup.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var currentOperation = operationsGroup[i];

                var previousOperationFailed =
                    this._currentOperationsGroupIndex > 0
                    && groupedOperationsToExecute[this._currentOperationsGroupIndex - 1][i].State != OperationState.Finished;
                if (previousOperationFailed)
                {
                    this.FinishOperation(currentOperation);

                    continue;
                }

                // This needs to be correctly disposed
                var stateChanger = currentOperation.StateChanged.Subscribe(_ => this.OperationOnStateChanged(currentOperation));
                var progressChanger = currentOperation.ProgressChanged.Subscribe(_ => this.UpdateProgress());

                //this.SubscribeToEvents(currentOperation);

                this.RunOperation(currentOperation, cancellationToken);
            }

            var groupExecutionResult = await this._taskCompletionSource.Task;
            if (!groupExecutionResult)
            {
                throw new OperationFailedException();
            }

            this._currentOperationsGroupIndex++;
        }

        this._currentOperationsGroup = null;
        this.SetFinalProgress();
    }

    /// <summary>
    /// Runs the operation.
    /// </summary>
    /// <param name="operation">The operation.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private void RunOperation(IInternalOperation operation, CancellationToken cancellationToken) =>
        Task
            .Run(() => operation.RunAsync(cancellationToken), cancellationToken)
            .ContinueWith(t =>
            {
                if (t.IsCanceled && operation.State == OperationState.NotStarted)
                {
                    this.FinishOperation(operation);
                }
            }, cancellationToken);

    /// <summary>
    /// Operations the on state changed.
    /// </summary>
    /// <param name="operation">The operation.</param>
    private async void OperationOnStateChanged(IInternalOperation operation)
    {

        if (operation.State is OperationState.Blocked)
        {
            var blockingOperation = (ISelfBlockingOperation)operation;
            await this.ProcessBlockedTaskAsync(blockingOperation);
            return;
        }

        if (operation.State.IsCompleted())
        {
            this.FinishOperation(operation);
        }

        this.UpdateProgress();
    }

    /// <summary>
    /// Finishes the operation.
    /// </summary>
    /// <param name="operation">The operation.</param>
    private void FinishOperation(IInternalOperation operation)
    {
        //this.UnsubscribeFromEvents(operation);

        var finishedOperationsCount = Interlocked.Increment(ref this._finishedOperationsCount);
        if (finishedOperationsCount != this._currentOperationsGroup?.Count)
        {
            return;
        }

        var isSuccessful = this._currentOperationsGroup.All(o => !operation.State.IsFailedOrCancelled());

        this._taskCompletionSource.SetResult(isSuccessful);
    }

    /// <summary>
    /// Processes the next blocked task async.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task ProcessNextBlockedTaskAsync()
    {
        string sourceFilePath;
        lock (this._locker)
        {
            var isBlockedFileAvailable = this._blockedFilesQueue.TryDequeue(out var currentBlockedFile);
            if (!isBlockedFileAvailable)
            {
                return;
            }

            sourceFilePath = currentBlockedFile.SourceFilePath;
        }

        var operation = this._blockedOperationsDictionary[sourceFilePath];
        this._blockedOperationsDictionary.Remove(sourceFilePath);

        await this.ProcessBlockedTaskAsync(operation);
    }

    /// <summary>
    /// Processes the blocked task async.
    /// </summary>
    /// <param name="operation">The operation.</param>
    /// <returns>A Task.</returns>
    private async Task ProcessBlockedTaskAsync(ISelfBlockingOperation operation)
    {
        if (this._cancellationTokenSource.IsCancellationRequested)
        {
            this.FinishOperation((IInternalOperation) operation);

            return;
        }

        if (this._continuationMode is null)
        {
            this._blockedOperationsDictionary[operation.CurrentBlockedFile.SourceFilePath] = operation;
            this._blockedFilesQueue.Enqueue(operation.CurrentBlockedFile);

            lock (this._locker)
            {
                if (this.CurrentBlockedFile == operation.CurrentBlockedFile && this._continuationMode is null)
                {
                    this._blockedChangedSubject.OnNext(true);
                }
            }
        }
        else
        {
            await this.ContinueWithDefaultOptionsAsync(operation, this._continuationMode.Value);
        }
    }

    /// <summary>
    /// Continues the with default options async.
    /// </summary>
    /// <param name="operation">The operation.</param>
    /// <param name="continuationMode">The continuation mode.</param>
    /// <returns>A Task.</returns>
    private async Task ContinueWithDefaultOptionsAsync(ISelfBlockingOperation operation,
        OperationContinuationMode continuationMode)
    {
        var (sourceFilePath, destinationFilePath) = operation.CurrentBlockedFile;
        OperationContinuationOptions options;
        if (continuationMode is OperationContinuationMode.Rename)
        {
            var newFilePath = this._fileNameGenerationService.GenerateFullName(destinationFilePath);
            options = OperationContinuationOptions.CreateRenamingContinuationOptions(
                sourceFilePath,
                true,
                newFilePath
            );
        }
        else
        {
            options = OperationContinuationOptions.CreateContinuationOptions(
                sourceFilePath,
                true,
                continuationMode
            );
        }

        await operation.ContinueAsync(options);
    }

    /// <summary>
    /// Operations the on progress changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void OperationOnProgressChanged(object sender, OperationProgressChangedEventArgs e) =>
        this.UpdateProgress();

    /// <summary>
    /// Updates the progress.
    /// </summary>
    private void UpdateProgress()
    {
        if (this._currentOperationsGroup is null)
        {
            return;
        }

        var finishedOperationGroupsProgress = (double)this._currentOperationsGroupIndex;
        var currentOperationGroupProgress = this._currentOperationsGroup.Sum(o => o.CurrentProgress) / this._totalOperationsCount;

        this.CurrentProgress = (finishedOperationGroupsProgress + currentOperationGroupProgress) / this._operationsGroupsCount;
    }
}