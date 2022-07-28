namespace RocketBlend.Presentation.Interfaces.Behaviors;

/// <summary>
/// The file system node properties behavior.
/// </summary>
public interface IFileSystemNodePropertiesBehavior
{
    /// <summary>
    /// Shows the properties async.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <returns>A Task.</returns>
    Task ShowPropertiesAsync(string directoryPath);
}