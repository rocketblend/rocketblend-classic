using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;
using RocketBlend.Services.Abstractions.Exceptions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.Operations;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The async operation state machine.
/// </summary>
public class AsyncOperationStateMachine : IOperation
{
    private readonly ICompositeOperation _compositeOperation;
    private readonly ILogger _logger;

    private readonly ISubject<OperationState> _stateChangedSubject = new Subject<OperationState>();

    private OperationState _operationState;
    private readonly object _locker;

    /// <inheritdoc />
    public OperationState State
    {
        get
        {
            lock (this._locker)
            {
                return this._operationState;
            }
        }
        private set
        {
            lock (this._locker)
            {
                if (this._operationState == value)
                {
                    return;
                }

                this._operationState = value;
            }

            this._stateChangedSubject.OnNext(value);
        }
    }

    /// <inheritdoc />
    public OperationInfo Info  => this._compositeOperation.Info;

    /// <inheritdoc />
    public (string SourceFilePath, string DestinationFilePath) CurrentBlockedFile =>
        this._compositeOperation.CurrentBlockedFile;

    /// <inheritdoc />
    public double CurrentProgress => this._compositeOperation.CurrentProgress;

    /// <inheritdoc />
    public IObservable<OperationState> StateChanged => this._stateChangedSubject.AsObservable();

    /// <inheritdoc />
    public IObservable<double> ProgressChanged => this._compositeOperation.ProgressChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncOperationStateMachine"/> class.
    /// </summary>
    /// <param name="compositeOperation">The composite operation.</param>
    /// <param name="logger">The logger.</param>
    public AsyncOperationStateMachine(
        ICompositeOperation compositeOperation,
        ILogger logger)
    {
        this._compositeOperation = compositeOperation;
        this._logger = logger;

        this._locker = new object();

        var blocked = this._compositeOperation.Blocked.Subscribe(this.CompositeOperationOnBlocked);
    }

    /// <inheritdoc />
    public Task RunAsync() =>
        this.ChangeStateAsync(OperationState.NotStarted, OperationState.InProgress);

    /// <inheritdoc />
    public Task ContinueAsync(OperationContinuationOptions options) =>
        this.ChangeStateAsync(OperationState.Blocked, OperationState.InProgress, options);

    /// <inheritdoc />
    public Task PauseAsync() =>
        this.ChangeStateAsync(OperationState.InProgress, OperationState.Pausing);

    /// <inheritdoc />
    public Task UnpauseAsync() =>
        this.ChangeStateAsync(OperationState.Paused, OperationState.Unpausing);

    /// <inheritdoc />
    public Task CancelAsync() =>
        this.ChangeStateAsync(this.State, OperationState.Cancelling);

    /// <summary>
    /// Changes the state async.
    /// </summary>
    /// <param name="expectedState">The expected state.</param>
    /// <param name="requestedState">The requested state.</param>
    /// <param name="options">The options.</param>
    /// <returns>A Task.</returns>
    private async Task ChangeStateAsync(
        OperationState expectedState,
        OperationState requestedState,
        OperationContinuationOptions? options = null)
    {
        var taskFactory = (this.State, requestedState) switch
        {
            _ when this.State == requestedState => GetCompletedTask,

            _ when this.State != expectedState =>
                throw new InvalidOperationException($"Inner state {this.State} is not {expectedState}"),

            (OperationState.NotStarted, OperationState.InProgress) =>
                this.WrapAsync(this._compositeOperation.RunAsync, OperationState.InProgress, OperationState.Finished),

            (_, OperationState.Cancelling) =>
                this.WrapAsync(this._compositeOperation.CancelAsync, OperationState.Cancelling, OperationState.Cancelled),

            (_, OperationState.Failed) when this.State != OperationState.Cancelling => GetCompletedTask, // TODO: cleanup?

            (OperationState.InProgress, OperationState.Pausing) =>
                this.WrapAsync(this._compositeOperation.PauseAsync, OperationState.Pausing, OperationState.Paused),

            (OperationState.Blocked, OperationState.InProgress) when options is null =>
                throw new ArgumentNullException(nameof(options)),

            (OperationState.Blocked, OperationState.InProgress) => () => this._compositeOperation.ContinueAsync(options),

            (OperationState.Paused, OperationState.Unpausing) =>
                this.WrapAsync(this._compositeOperation.UnpauseAsync, OperationState.Unpausing, OperationState.InProgress),

            (OperationState.Cancelling, OperationState.Cancelled) => GetCompletedTask,
            (OperationState.InProgress, OperationState.Finished) => GetCompletedTask,
            (OperationState.InProgress, OperationState.Blocked) => GetCompletedTask,
            (OperationState.Unpausing, OperationState.InProgress) => GetCompletedTask,
            (OperationState.Pausing, OperationState.Paused) => GetCompletedTask,

            _ => throw new InvalidOperationException($"{this.State} has no transition to {requestedState}")
        };

        this.State = requestedState;

        await taskFactory();
    }

    /// <summary>
    /// Wraps the async.
    /// </summary>
    /// <param name="taskFactory">The task factory.</param>
    /// <param name="expectedState">The expected state.</param>
    /// <param name="requestedState">The requested state.</param>
    /// <returns>A Func.</returns>
    private Func<Task> WrapAsync(Func<Task> taskFactory,
        OperationState expectedState, OperationState requestedState) =>
        async () =>
        {
            try
            {
                await taskFactory();
                if (this.State == expectedState)
                {
                    await this.ChangeStateAsync(expectedState, requestedState);
                }
            }
            catch (OperationFailedException ex)
            {
                this._logger.LogError(
                    $"{nameof(AsyncOperationStateMachine)} {nameof(OperationFailedException)} occurred: {ex}");

                await this.ChangeStateAsync(this.State, OperationState.Failed);
            }
            catch (Exception ex)
            {
                this._logger.LogError(
                    $"{nameof(AsyncOperationStateMachine)} {nameof(Exception)} occurred: {ex}");
            }
        };

    /// <summary>
    /// Gets the completed task.
    /// </summary>
    /// <returns>A Task.</returns>
    private static Task GetCompletedTask() => Task.CompletedTask;

    /// <summary>
    /// Composites the operation on blocked.
    /// </summary>
    private async void CompositeOperationOnBlocked(bool blocked)
    {
        if(blocked)
        {
            await this.ChangeStateAsync(OperationState.InProgress, OperationState.Blocked);
        }
    }
}