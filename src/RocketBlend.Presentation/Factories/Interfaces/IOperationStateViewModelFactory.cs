using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.Factories.Interfaces;

/// <summary>
/// The operation state view model factory interface.
/// </summary>
public interface IOperationStateViewModelFactory
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="operation">The operation.</param>
    /// <returns>An IOperationStateViewModel.</returns>
    IOperationStateViewModel Create(IOperation operation);
}