using System.Text.RegularExpressions;
using HtmlAgilityPack;
using RocketBlend.WebScraper.Blender.Core.Enums;
using RocketBlend.WebScraper.Blender.Core.Interfaces;
using RocketBlend.WebScraper.Blender.Core.Models;

namespace RocketBlend.WebScraper.Blender;

/// <summary>
/// The blender scraper service.
/// </summary>
public class BlenderBuildScraperService : IBlenderBuildScraperService
{
    private static readonly string ReleaseBuildsUrl = "https://download.blender.org/release";
    private static readonly string ArchivedBuildsUrl = "https://builder.blender.org/download";
    private static readonly string Unknown = "Unknown";

    private static readonly double OldestVersion = 2.79;

    /// <inheritdoc />
    public async Task<IEnumerable<BlenderBuildInfo>> ScrapeArchivedBuilds(BuildPlatform buildPlatform, params ArchivedBuildType[] archivedBuildTypes)
    {
        List<BlenderBuildInfo> builds = new();

        foreach (var buildType in archivedBuildTypes)
        {
            string path = $"{ArchivedBuildsUrl}/{buildType.ToString().ToLower()}/archive";
            HtmlDocument document = await FetchPageAsync(path).ConfigureAwait(false);

            Regex regex = GetRegexForPlatform(buildPlatform);

            // HtmlNode node = document.DocumentNode.SelectSingleNode("//div[@class='a']");
            foreach (HtmlNode link in document.DocumentNode.SelectNodes("//a[@href][contains(@class,'build-title')]").Where(a => regex.IsMatch(a.Attributes["href"].Value)))
            {
                string buildUrl = link.Attributes["href"].Value;
                string buildRef = link.FirstChild.InnerText.Replace(" - ", string.Empty);
                string buildTag = link.LastChild.InnerText;

                HtmlNode details = link.ParentNode.ParentNode;

                string buildHash = details.SelectSingleNode(".//a[@title='See history']").InnerText;
                string buildFileSize = details.SelectSingleNode(".//li[@title='File size']").InnerText;

                builds.Add(new BlenderBuildInfo(
                    buildPlatform,
                    buildUrl,
                    buildRef,
                    buildTag,
                    buildHash,
                    buildFileSize));
            }
        }

        return builds.OrderByDescending(b => b.Name);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BlenderBuildInfo>> ScrapeStableReleaseBuilds(BuildPlatform buildPlatform)
    {
        List<BlenderBuildInfo> builds = new();

        HtmlDocument document = await FetchPageAsync(ReleaseBuildsUrl).ConfigureAwait(false);

        Regex matchNameRegex = new("Blender\\d+\\.\\d+", RegexOptions.IgnoreCase);
        Regex matchVersionRegex = new("\\d+\\.\\d+", RegexOptions.IgnoreCase);

        var releases = document.DocumentNode.SelectNodes(".//a[@href]").Where(
            a => matchNameRegex.IsMatch(a.Attributes["href"].Value) &&
            (double.Parse(matchVersionRegex.Match(a.Attributes["href"].Value).Groups[0].Value) >= OldestVersion));

        foreach (HtmlNode link in releases)
        {
            string releasePath = $"{ReleaseBuildsUrl}/{link.Attributes["href"].Value}";

            HtmlDocument page = await FetchPageAsync(releasePath).ConfigureAwait(false);

            Regex regexPlatform = GetRegexForPlatform(buildPlatform);

            var buildFile = page.DocumentNode.SelectNodes(".//a[@href]").FirstOrDefault(a => regexPlatform.IsMatch(a.Attributes["href"].Value));

            if (buildFile != null)
            {
                builds.Add(new BlenderBuildInfo(
                buildPlatform,
                $"{releasePath}{buildFile.Attributes["href"].Value}",
                link.InnerText.Replace("/", string.Empty),
                "Stable",
                Unknown,
                Unknown));
            }
        }

        return builds.OrderByDescending(b => b.Name);
    }

    /// <summary>
    /// Gets the regex for platform.
    /// </summary>
    /// <param name="buildPlatform">The build platform.</param>
    /// <returns>A Regex.</returns>
    private static Regex GetRegexForPlatform(BuildPlatform buildPlatform)
    {
        string expression;

        switch (buildPlatform)
        {
            case BuildPlatform.Windows:
                expression = "blender-.+win.+64.+zip$";
                break;

            case BuildPlatform.Linux:
                expression = "blender-.+lin.+64.+tar+(?!.*sha256).*";
                break;

            case BuildPlatform.MacOs:
                expression = "blender-.+(macOS|darwin).+dmg$";
                break;

            default:
                throw new ArgumentException("Invalid platform.");
        }

        return new Regex(expression, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Fetches the page async.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <returns>A Task.</returns>
    private static async Task<HtmlDocument> FetchPageAsync(string url)
    {
        var web = new HtmlWeb();
        return await web.LoadFromWebAsync(url).ConfigureAwait(false);
    }
}