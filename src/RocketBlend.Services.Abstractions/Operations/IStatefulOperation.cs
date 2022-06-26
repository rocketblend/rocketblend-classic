using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The stateful operation.
/// </summary>
public interface IStatefulOperation
{
    /// <summary>
    /// Gets the state.
    /// </summary>
    OperationState State { get; }

    /// <summary>
    /// Gets the state changed.
    /// </summary>
    public IObservable<OperationState> StateChanged { get; }
}