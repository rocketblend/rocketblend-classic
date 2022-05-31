namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The environment path service.
/// </summary>
public interface IEnvironmentPathService
{
    /// <summary>
    /// Gets the directory name.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string GetDirectoryName(string path);

    /// <summary>
    /// Combines the.
    /// </summary>
    /// <param name="path1">The path1.</param>
    /// <param name="path2">The path2.</param>
    /// <returns>A string.</returns>
    string Combine(string path1, string path2);

    /// <summary>
    /// Gets the relative path.
    /// </summary>
    /// <param name="relativeTo">The relative to.</param>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string GetRelativePath(string relativeTo, string path);

    /// <summary>
    /// Gets the file name without extension.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string GetFileNameWithoutExtension(string path);

    /// <summary>
    /// Gets the file name.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string GetFileName(string path);

    /// <summary>
    /// Gets the extension.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string GetExtension(string path);
}