using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Extensions;

/// <summary>
/// The operation state extensions.
/// </summary>
public static class OperationStateExtensions
{
    /// <summary>
    /// Are the completed.
    /// </summary>
    /// <param name="operationState">The operation state.</param>
    /// <returns>A bool.</returns>
    public static bool IsCompleted(this OperationState operationState)
    {
        var completedOperationStates = new[]
        {
            OperationState.Failed,
            OperationState.Cancelled,
            OperationState.Finished,
            OperationState.Skipped
        };

        return completedOperationStates.Contains(operationState);
    }

    /// <summary>
    /// Are the failed or cancelled.
    /// </summary>
    /// <param name="operationState">The operation state.</param>
    /// <returns>A bool.</returns>
    public static bool IsFailedOrCancelled(this OperationState operationState) =>
        operationState is OperationState.Failed || operationState is OperationState.Cancelled;
}