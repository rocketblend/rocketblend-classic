namespace RocketBlend.Services.Abstractions.Models.Enums;

/// <summary>
/// The operation state.
/// </summary>
public enum OperationState : byte
{
    NotStarted,
    InProgress,
    Paused,
    Pausing,
    Unpausing,
    Blocked,
    Finished,
    Cancelling,
    Cancelled,
    Failed,
    Skipped
}