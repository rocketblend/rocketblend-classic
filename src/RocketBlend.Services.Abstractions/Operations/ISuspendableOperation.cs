namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The suspendable operation interface.
/// </summary>
public interface ISuspendableOperation
{
    /// <summary>
    /// Pauses the async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task PauseAsync();

    /// <summary>
    /// Unpauses the async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task UnpauseAsync();
}