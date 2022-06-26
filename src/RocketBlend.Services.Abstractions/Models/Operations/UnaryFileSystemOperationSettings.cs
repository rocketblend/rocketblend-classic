namespace RocketBlend.Services.Abstractions.Models.Operations;

/// <summary>
/// The unary file system operation settings.
/// </summary>
public class UnaryFileSystemOperationSettings
{
    /// <summary>
    /// Gets the top level directories.
    /// </summary>
    public IReadOnlyList<string> TopLevelDirectories { get; }

    /// <summary>
    /// Gets the top level files.
    /// </summary>
    public IReadOnlyList<string> TopLevelFiles { get; }

    /// <summary>
    /// Gets the source directory.
    /// </summary>
    public string SourceDirectory { get; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryFileSystemOperationSettings"/> class.
    /// </summary>
    /// <param name="topLevelDirectories">The top level directories.</param>
    /// <param name="topLevelFiles">The top level files.</param>
    /// <param name="sourceDirectory">The source directory.</param>
    public UnaryFileSystemOperationSettings(
        IReadOnlyList<string> topLevelDirectories,
        IReadOnlyList<string> topLevelFiles,
        string sourceDirectory)
    {
        this.TopLevelDirectories = topLevelDirectories;
        this.TopLevelFiles = topLevelFiles;
        this.SourceDirectory = sourceDirectory;
    }
}