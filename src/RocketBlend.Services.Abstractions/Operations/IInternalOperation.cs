namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The internal operation.
/// </summary>
public interface IInternalOperation : IOperationWithProgress, IStatefulOperation
{
    /// <summary>
    /// Runs the async.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task RunAsync(CancellationToken cancellationToken);
}