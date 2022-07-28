using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Services.Installs;

/// <summary>
/// The blender install service.
/// </summary>
public class BlenderInstallService : IBlenderInstallService
{
    private readonly IBlenderInstallStateService _blenderInstallStateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallService"/> class.
    /// </summary>
    /// <param name="blenderInstallStateService">The blender install state service.</param>
    public BlenderInstallService(
        IBlenderInstallStateService blenderInstallStateService)
    {
        this._blenderInstallStateService = blenderInstallStateService;
    }

    /// <inheritdoc />
    public async Task CreateInstall(BlenderInstallModel install)
    {
        await this._blenderInstallStateService.AddOrUpdateInstall(install);
    }

    /// <inheritdoc />
    public async Task UpdateInstall(BlenderInstallModel install)
    {
        await this._blenderInstallStateService.AddOrUpdateInstall(install);
    }

    /// <inheritdoc />
    public async Task DeleteInstall(Guid installId)
    {
        await this._blenderInstallStateService.RemoveInstall(installId);
    }

    /// <inheritdoc />
    public void CreateBlendFile(BlenderInstallModel install, string path)
    {
        throw new NotImplementedException();
    }
}