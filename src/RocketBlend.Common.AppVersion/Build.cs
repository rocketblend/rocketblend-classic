using System.Text.Json;

namespace RocketBlend.Common.Infrastructure;

public record BuildInfo(string BranchName, string BuildNumber, string BuildId, string CommitHash);

/// <summary>
/// APP version info.
/// </summary>
public static class Build
{
    /// <summary>
    /// The build file name.
    /// </summary>
    private const string BuildFileName = "buildinfo.json";

    private static BuildInfo _fileBuildInfo = new(
        BranchName: string.Empty,
        BuildNumber: DateTime.UtcNow.ToString("yyyyMMdd") + ".0",
        BuildId: "xxxxxx",
        CommitHash: $"Not yet initialised - call {nameof(InitialiseBuildInfoGivenPath)}");

    /// <summary>
    /// Initialises the build info given path.
    /// </summary>
    /// <param name="path">The path.</param>
    public static void InitialiseBuildInfoGivenPath(string path)
    {
        var buildFilePath = Path.Combine(path, BuildFileName);
        if (File.Exists(buildFilePath))
        {
            try
            {
                var buildInfoJson = File.ReadAllText(buildFilePath);
                var buildInfo = JsonSerializer.Deserialize<BuildInfo>(buildInfoJson, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                if (buildInfo == null)
                {
                    throw new Exception($"Failed to deserialise {BuildFileName}");
                }

                _fileBuildInfo = buildInfo;
            }
            catch (Exception)
            {
                _fileBuildInfo = new BuildInfo(
                    BranchName: string.Empty,
                    BuildNumber: DateTime.UtcNow.ToString("yyyyMMdd") + ".0",
                    BuildId: "xxxxxx",
                    CommitHash: "Failed to load build info from buildinfo.json");
            }
        }
    }

    /// <summary>
    /// Gets the build info.
    /// </summary>
    /// <returns>A BuildInfo.</returns>
    public static BuildInfo GetBuildInfo() => _fileBuildInfo;
}
