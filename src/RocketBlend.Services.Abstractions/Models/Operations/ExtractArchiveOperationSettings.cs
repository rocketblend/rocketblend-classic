using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models.Operations;

/// <summary>
/// The extract archive operation settings.
/// </summary>
public class ExtractArchiveOperationSettings
{
    /// <summary>
    /// Gets the input top level file.
    /// </summary>
    public string InputTopLevelFile { get; }

    /// <summary>
    /// Gets the target directory.
    /// </summary>
    public string TargetDirectory { get; }

    /// <summary>
    /// Gets the archive type.
    /// </summary>
    public ArchiveType ArchiveType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtractArchiveOperationSettings"/> class.
    /// </summary>
    /// <param name="inputTopLevelFile">The input top level file.</param>
    /// <param name="targetDirectory">The target directory.</param>
    /// <param name="archiveType">The archive type.</param>
    public ExtractArchiveOperationSettings(
        string inputTopLevelFile,
        string targetDirectory,
        ArchiveType archiveType)
    {
        this.InputTopLevelFile = inputTopLevelFile;
        this.TargetDirectory = targetDirectory;
        this.ArchiveType = archiveType;
    }
}