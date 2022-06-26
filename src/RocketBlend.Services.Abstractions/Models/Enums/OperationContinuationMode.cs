namespace RocketBlend.Services.Abstractions.Models.Enums;

/// <summary>
/// The operation continuation mode.
/// </summary>
public enum OperationContinuationMode : byte
{
    Skip,
    Overwrite,
    OverwriteIfOlder,
    Rename
}