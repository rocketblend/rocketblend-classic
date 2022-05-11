using RocketBlend.WebScraper.Blender.Core.Enums;

namespace RocketBlend.WebScraper.Blender.Core.Models;

public record BlenderBuildInfo(BuildPlatform Platform, string DownloadUrl, string Name, string Tag, string Hash, string Filesize);