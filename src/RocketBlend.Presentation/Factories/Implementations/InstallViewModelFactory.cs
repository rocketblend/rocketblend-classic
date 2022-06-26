using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.ViewModels.Main.Installs;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.Factories.Implementations;

/// <summary>
/// The install view model factory.
/// </summary>
public class InstallViewModelFactory : IInstallViewModelFactory
{
    readonly IBlenderInstallService _blenderInstallService;
    readonly IOperationsService _operationsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallViewModelFactory"/> class.
    /// </summary>
    /// <param name="blenderInstallService">The blender install service.</param>
    /// <param name="operationsService">The operations service.</param>
    public InstallViewModelFactory(
        IBlenderInstallService blenderInstallService,
        IOperationsService operationsService)
    {
        this._blenderInstallService = blenderInstallService;
        this._operationsService = operationsService;
    }

    /// <inheritdoc />
    public IInstallViewModel Create(BlenderInstallModel model)
    {
        return new InstallViewModel(
            this._blenderInstallService,
            this._operationsService,
            model);
    }
}
