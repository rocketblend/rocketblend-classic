using RocketBlend.WebScraper.Blender.Core.Enums;
using RocketBlend.WebScraper.Blender.Core.Models;

namespace RocketBlend.WebScraper.Blender.Core.Interfaces;

/// <summary>
/// The blender scraper service.
/// </summary>
public interface IBlenderBuildScraperService
{
    /// <summary>
    /// Scrapes the stable release builds.
    /// </summary>
    public Task<IEnumerable<BlenderBuildInfo>> ScrapeStableReleaseBuilds(BuildPlatform buildPlatform);

    /// <summary>
    /// Scrapes the archived builds.
    /// </summary>
    /// <param name="archivedBuildType">The archived build type.</param>
    public Task<IEnumerable<BlenderBuildInfo>> ScrapeArchivedBuilds(BuildPlatform buildPlatform, params ArchivedBuildType[] archivedBuildType);
}