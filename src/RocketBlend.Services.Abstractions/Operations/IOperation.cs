using System.ComponentModel;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The operation.
/// </summary>
public interface IOperation : ISuspendableOperation, IOperationWithProgress, IOperationWithInfo,
    ISelfBlockingOperation, IStatefulOperation
{
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