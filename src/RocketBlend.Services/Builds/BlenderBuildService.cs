using System.Reactive.Linq;
using Akavache;
using DynamicData;
using RocketBlend.Services.Abstractions.Builds;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.WebScraper.Blender.Core.Enums;
using RocketBlend.WebScraper.Blender.Core.Interfaces;

namespace RocketBlend.Services.Builds;

/// <summary>
/// The blender build service.
/// </summary>
public class BlenderBuildService : IBlenderBuildService
{
    /// <summary>
    /// The blender builds key.
    /// </summary>
    private const string BlenderBuildsKey = "BlenderBuilds";
    
    private readonly IBlenderBuildScraperService _blenderBuildScraperService;

    private readonly IBlobCache _blobCache;

    private readonly SourceCache<BlenderBuildModel, Guid> _items = new(x => x.Id);

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderBuildService"/> class.
    /// </summary>
    /// <param name="blenderBuildScraperService">The blender build scraper service.</param>
    public BlenderBuildService(IBlenderBuildScraperService blenderBuildScraperService)
    {
        this._blenderBuildScraperService = blenderBuildScraperService;
        this._blobCache = BlobCache.LocalMachine;

        this.RefreshSourceCache().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public IObservable<IChangeSet<BlenderBuildModel, Guid>> Connect() => this._items.Connect();

    /// <inheritdoc />
    public async Task Refresh()
    {
        await this._blobCache.Invalidate(BlenderBuildsKey);
        await this.RefreshSourceCache().ConfigureAwait(false);
    }

    /// <summary>
    /// Refreshes the source cache.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task RefreshSourceCache()
    {
        var builds = await this.GetBuilds();
        if (builds != null)
        {
            this._items.Edit(cache =>
            {
                cache.Clear();
                cache.AddOrUpdate(builds);
            });
        }
    }

    /// <summary>
    /// Gets the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task<List<BlenderBuildModel>> GetBuilds()
    {
        return await this._blobCache.GetOrFetchObject(
            BlenderBuildsKey,
            async () => await this.ScrapeBuilds().ConfigureAwait(false),
            DateTimeOffset.Now.AddDays(1));
    }

    /// <summary>
    /// Scrapes the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task<List<BlenderBuildModel>> ScrapeBuilds()
    {
        // TODO: Change build platform based on OS.
        var scrapedBuild = await this._blenderBuildScraperService.ScrapeStableReleaseBuilds(BuildPlatform.Windows).ConfigureAwait(false);
        return scrapedBuild.Select(x => new BlenderBuildModel()
        {
            Name = x.Name,
            DownloadUrl = x.DownloadUrl,
            Hash = x.Hash, // Not fetch atm.
            Tag = x.Tag,
        }).ToList();
    }
}