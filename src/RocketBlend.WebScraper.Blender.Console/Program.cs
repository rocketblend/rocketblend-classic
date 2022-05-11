// See https://aka.ms/new-console-template for more information
using RocketBlend.WebScraper.Blender;
using RocketBlend.WebScraper.Blender.Core.Enums;

BlenderBuildScraperService scraper = new();

var archievedUrls = await scraper.ScrapeArchivedBuilds(BuildPlatform.Windows, ArchivedBuildType.Daily);
var releaseUrls = await scraper.ScrapeStableReleaseBuilds(BuildPlatform.Windows);

Console.WriteLine("-----------START------------");
foreach (var url in archievedUrls)
{
    Console.WriteLine(url);
}
Console.WriteLine("-----------END------------");

Console.WriteLine("-----------START------------");
foreach (var url in releaseUrls)
{
    Console.WriteLine(url);
}
Console.WriteLine("-----------END------------");