using RocketBlend.Services.Abstractions.Models.Operations;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The self blocking operation.
/// </summary>
public interface ISelfBlockingOperation
{
    /// <summary>
    /// Gets the current blocked file.
    /// </summary>
    (string SourceFilePath, string DestinationFilePath) CurrentBlockedFile { get; }

    /// <summary>
    /// Continues the async.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <returns>A Task.</returns>
    Task ContinueAsync(OperationContinuationOptions options);
}