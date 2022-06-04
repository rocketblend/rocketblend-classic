using System.Reactive.Linq;
using Akavache;
using RocketBlend.Extensions;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Abstractions.Models.EventArgs;

namespace RocketBlend.Services;

/// <summary>
/// The blender install service.
/// </summary>
public class BlenderInstallService : IBlenderInstallService
{
    /// <summary>
    /// The blender installs key.
    /// </summary>
    private const string BlenderInstallsKey = "BlenderInstalls";

    private readonly List<BlenderInstallModel> _installs;

    /// <inheritdoc />
    public IReadOnlyCollection<BlenderInstallModel> Installs => this._installs;

    /// <inheritdoc />
    public event EventHandler<BlenderInstallsListChangedEventArgs>? InstallAdded;

    /// <inheritdoc />
    public event EventHandler<BlenderInstallsListChangedEventArgs>? InstallRemoved;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallService"/> class.
    /// </summary>
    public BlenderInstallService()
    {
        this._installs = this.GetInstalls();
    }

    /// <inheritdoc />
    public void AddInstall(BlenderInstallModel install)
    {
        if (this._installs.Contains(install))
        {
            return;
        }

        this._installs.Add(install);
        this.SaveInstalls();
        this.InstallAdded.Raise(this, CreateArgs(install));
    }

    /// <inheritdoc />
    public void RemoveInstall(BlenderInstallModel install)
    {
        if (this._installs.Remove(install))
        {
            this.SaveInstalls();
            InstallRemoved.Raise(this, CreateArgs(install));
        }
    }

    /// <summary>
    /// Gets the installs.
    /// </summary>
    /// <returns>A Task.</returns>
    private List<BlenderInstallModel> GetInstalls()
    {
        return BlobCache.LocalMachine.GetObject<List<BlenderInstallModel>>(BlenderInstallsKey).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Saves the installs.
    /// </summary>
    private void SaveInstalls()
    {
        BlobCache.LocalMachine.InsertObject(BlenderInstallsKey, this.Installs.ToList());
    }

    /// <summary>
    /// Creates the args.
    /// </summary>
    /// <param name="install">The install.</param>
    /// <returns>A BlenderInstallsListChangedEventArgs.</returns>
    private static BlenderInstallsListChangedEventArgs CreateArgs(BlenderInstallModel install) => new(install);
}