using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services;

/// <summary>
/// The path service.
/// </summary>
public class PathService : IPathService
{
    private readonly IEnvironmentPathService _environmentPathService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PathService"/> class.
    /// </summary>
    /// <param name="environmentPathService">The environment path service.</param>
    public PathService(IEnvironmentPathService environmentPathService)
    {
        this._environmentPathService = environmentPathService;
    }

    /// <inheritdoc />
    public string GetCommonRootDirectory(IReadOnlyList<string> paths)
    {
        if (!paths.Any())
        {
            return null;
        }

        var commonPrefix = new string(
            paths[0][..paths.Select(this.RightTrimPathSeparators).Min(s => s.Length)]
                .TakeWhile((c, i) => paths.All(s => s[i] == c)).ToArray()
        );

        return new[] {'/', '\\'}.Contains(commonPrefix[^1])
            ? this.RightTrimPathSeparators(commonPrefix)
            : this.GetParentDirectory(commonPrefix);
    }

    /// <inheritdoc />
    public string GetParentDirectory(string path) => this._environmentPathService.GetDirectoryName(path);

    /// <inheritdoc />
    public string Combine(string path1, string path2) => this._environmentPathService.Combine(path1, path2);

    /// <inheritdoc />
    public string GetRelativePath(string relativeTo, string path) =>
        this._environmentPathService.GetRelativePath(relativeTo, path);

    /// <inheritdoc />
    public string GetFileNameWithoutExtension(string path)
    {
        if (!path.StartsWith("."))
        {
            return this._environmentPathService.GetFileNameWithoutExtension(path);
        }

        if (path.Count(c => c == '.') == 1)
        {
            return path;
        }

        var lastDot = path.LastIndexOf(".", StringComparison.InvariantCulture);

        return path[..lastDot];
    }

    /// <inheritdoc />
    public string GetFileName(string path)
    {
        var fileName = this._environmentPathService.GetFileName(path);

        return string.IsNullOrEmpty(fileName) ? path : fileName;
    }

    /// <inheritdoc />
    public string GetExtension(string path)
    {
        if (path.StartsWith("."))
        {
            if (path.Count(c => c == '.') == 1)
            {
                return string.Empty;
            }

            var lastDot = path.LastIndexOf(".", StringComparison.InvariantCulture);

            return path[(lastDot + 1)..];
        }

        var extension = this._environmentPathService.GetExtension(path);

        return extension.StartsWith(".") ? extension[1..] : extension;
    }

    /// <inheritdoc />
    public string RightTrimPathSeparators(string path) => path == "/" ? path : path.TrimEnd('/').TrimEnd('\\');

    /// <inheritdoc />
    public string LeftTrimPathSeparators(string relativePath) => relativePath.TrimStart('/').TrimStart('\\');
}