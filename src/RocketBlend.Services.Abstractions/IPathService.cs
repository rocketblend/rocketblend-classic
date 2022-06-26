namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The path service.
/// </summary>
public interface IPathService
{
    /// <summary>
    /// Gets the common root directory.
    /// </summary>
    /// <param name="paths">The paths.</param>
    /// <returns>A string.</returns>
    string GetCommonRootDirectory(IReadOnlyList<string> paths);

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
    /// Gets the parent directory.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string GetParentDirectory(string path);

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

    /// <summary>
    /// Rights the trim path separators.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A string.</returns>
    string RightTrimPathSeparators(string path);

    /// <summary>
    /// Lefts the trim path separators.
    /// </summary>
    /// <param name="relativePath">The relative path.</param>
    /// <returns>A string.</returns>
    string LeftTrimPathSeparators(string relativePath);
}