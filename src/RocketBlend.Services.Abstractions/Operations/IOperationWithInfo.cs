using RocketBlend.Services.Abstractions.Models.Operations;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The operation with info.
/// </summary>
public interface IOperationWithInfo
{
    /// <summary>
    /// Gets the info.
    /// </summary>
    OperationInfo Info { get; }
}