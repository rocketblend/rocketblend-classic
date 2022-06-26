namespace RocketBlend.Services.Abstractions.Operations;
/// <summary>
/// The composite operation.
/// </summary>

public interface ICompositeOperation : ISuspendableOperation, IOperationWithProgress, IOperationWithInfo,
    ISelfBlockingOperation
{
    /// <summary>
    /// Gets the blocked.
    /// </summary>
    public IObservable<bool> Blocked { get; }

    /// <summary>
    /// Runs the async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task RunAsync();

    /// <summary>
    /// Cancels the async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task CancelAsync();
}