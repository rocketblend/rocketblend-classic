using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Presentation.ViewModels.Main.OperationsStates;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.Factories.Implementations;

/// <summary>
/// The operation state view model factory.
/// </summary>
public class OperationStateViewModelFactory : IOperationStateViewModelFactory
{
    private readonly IPathService _pathService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationStateViewModelFactory"/> class.
    /// </summary>
    /// <param name="pathService">The path service.</param>
    public OperationStateViewModelFactory(IPathService pathService)
    {
        this._pathService = pathService;
    }

    /// <inheritdoc />
    public IOperationStateViewModel Create(IOperation operation) =>
        new OperationStateViewModel(this._pathService, operation);
}