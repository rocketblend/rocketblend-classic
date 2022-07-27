using System.Reactive.Linq;
using Akavache;
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

    private readonly List<BlenderBuildModel> _builds = new();

    /// <inheritdoc />
    public IReadOnlyCollection<BlenderBuildModel> BlenderBuilds => this._builds;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderBuildService"/> class.
    /// </summary>
    /// <param name="blenderBuildScraperService">The blender build scraper service.</param>
    public BlenderBuildService(IBlenderBuildScraperService blenderBuildScraperService)
    {
        this._blenderBuildScraperService = blenderBuildScraperService;
        this._blobCache = BlobCache.LocalMachine;
    }

    /// <summary>
    /// Initializes the.
    /// </summary>
    /// <returns>A Task.</returns>
    public async Task Initialize()
    {
        await this.UpdateBuilds();
    }

    /// <inheritdoc />
    public async Task Refresh()
    {
        await this._blobCache.InvalidateObject<List<BlenderBuildModel>>(BlenderBuildsKey);
        await this.UpdateBuilds();
    }

    /// <summary>
    /// Updates the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task UpdateBuilds()
    {
        this._builds.Clear();
        this._builds.AddRange(await this.GetBuilds());
    }

    /// <summary>
    /// Scrapes the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task<List<BlenderBuildModel>> ScrapeBuilds()
    {
        // TODO: Change build platform based on OS.
        var scrapedBuild = await this._blenderBuildScraperService.ScrapeStableReleaseBuilds(BuildPlatform.Windows);
        return scrapedBuild.Select(x => new BlenderBuildModel()
        {
            Name = x.Name,
            DownloadUrl = x.DownloadUrl,
            Hash = x.Hash,
            Tag = x.Tag,
        }).ToList();
    }

    /// <summary>
    /// Gets the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task<List<BlenderBuildModel>> GetBuilds()
    {
        return await this._blobCache.GetOrFetchObject(
            BlenderBuildsKey,
            async () => await this.ScrapeBuilds(),
            DateTimeOffset.Now.AddHours(1));
    }
}
