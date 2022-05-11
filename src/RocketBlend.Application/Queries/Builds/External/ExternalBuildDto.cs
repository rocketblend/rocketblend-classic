namespace RocketBlend.Application.Queries.Builds.External;

public record ExternalBuildDto(string Name, string Tag, string DownloadUrl, string Hash, string Filesize);
