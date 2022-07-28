using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.ViewModels.Main.Installs;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Installs;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.Factories.Implementations;

/// <summary>
/// The install view model factory.
/// </summary>
public class InstallViewModelFactory : IInstallViewModelFactory
{
    private readonly IBlenderInstallStateService _blenderInstallStateService;
    private readonly IOperationsService _operationsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallViewModelFactory"/> class.
    /// </summary>
    /// <param name="blenderInstallStateService">The blender install service.</param>
    /// <param name="operationsService">The operations service.</param>
    public InstallViewModelFactory(
        IBlenderInstallStateService blenderInstallStateService,
        IOperationsService operationsService)
    {
        this._blenderInstallStateService = blenderInstallStateService;
        this._operationsService = operationsService;
    }

    /// <inheritdoc />
    public IInstallViewModel Create(BlenderInstallModel model)
    {
        return new InstallViewModel(
            this._blenderInstallStateService,
            this._operationsService,
            model);
    }
}