using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment path service.
/// </summary>
public class EnvironmentPathService : IEnvironmentPathService
{
    /// <inheritdoc />
    public string GetDirectoryName(string path) => Path.GetDirectoryName(path);

    /// <inheritdoc />
    public string Combine(string path1, string path2) => Path.Combine(path1, path2);

    /// <inheritdoc />
    public string GetRelativePath(string relativeTo, string path) => Path.GetRelativePath(relativeTo, path);

    /// <inheritdoc />
    public string GetFileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);

    /// <inheritdoc />
    public string GetFileName(string path) => Path.GetFileName(path);

    /// <inheritdoc />
    public string GetExtension(string path) => Path.GetExtension(path);
}