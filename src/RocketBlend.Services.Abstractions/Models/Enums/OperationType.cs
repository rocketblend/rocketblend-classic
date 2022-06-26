namespace RocketBlend.Services.Abstractions.Models.Enums;

/// <summary>
/// The operation type.
/// </summary>
public enum OperationType : byte
{
    Copy,
    Move,
    Delete,
    Pack,
    Extract,
    Download,
    InstallBlender
}