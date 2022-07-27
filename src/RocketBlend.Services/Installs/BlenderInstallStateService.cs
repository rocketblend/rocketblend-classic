using System.Reactive.Linq;
using Akavache;
using DynamicData;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Services.Installs;

/// <summary>
/// The blender install state service.
/// </summary>
public class BlenderInstallStateService : IBlenderInstallStateService
{
    /// <summary>
    /// The blender installs key.
    /// </summary>
    private const string BlenderInstallsKey = "BlenderInstalls";

    private readonly IBlobCache _blobCache;

    private readonly SourceCache<BlenderInstallModel, Guid> _items = new(x => x.Id);

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallStateService"/> class.
    /// </summary>
    public BlenderInstallStateService()
    {
        this._blobCache = BlobCache.LocalMachine;

        var installs = this.GetInstalls();
        if (installs != null)
        {
            this._items.Edit(cache =>
            {
                cache.Clear();
                cache.AddOrUpdate(installs);
            });
        }
    }

    /// <inheritdoc />
    public IObservable<IChangeSet<BlenderInstallModel, Guid>> Connect() => this._items.Connect();

    /// <inheritdoc />
    public async Task AddOrUpdateInstall(BlenderInstallModel install)
    {
        this._items.AddOrUpdate(install);
        await this.Save();
    }

    /// <inheritdoc />
    public async Task RemoveInstall(Guid id)
    {
        this._items.RemoveKey(id);
        await this.Save();
    }

    /// <inheritdoc />
    public BlenderInstallModel? GetInstall(Guid installId)
    {
        var item = this._items.Lookup(installId);
        return item.HasValue ? item.Value : null;
    }

    /// <summary>
    /// Saves the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Save()
    {
        await this._blobCache.InsertObject(BlenderInstallsKey, this._items.Items);
    }

    /// <summary>
    /// Gets the installs.
    /// </summary>
    /// <returns>An IEnumerable&lt;BlenderInstallModel&gt;? .</returns>
    private IEnumerable<BlenderInstallModel>? GetInstalls()
    {
        try
        {
            return this._blobCache.GetObject<IEnumerable<BlenderInstallModel>>(BlenderInstallsKey).GetAwaiter().Wait();
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
