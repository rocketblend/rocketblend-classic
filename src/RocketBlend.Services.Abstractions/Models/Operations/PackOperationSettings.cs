using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models.Operations;

/// <summary>
/// The pack operation settings.
/// </summary>
public class PackOperationSettings
{
    /// <summary>
    /// Gets the input top level directories.
    /// </summary>
    public IReadOnlyList<string> InputTopLevelDirectories { get; }

    /// <summary>
    /// Gets the input top level files.
    /// </summary>
    public IReadOnlyList<string> InputTopLevelFiles { get; }

    /// <summary>
    /// Gets the output top level file.
    /// </summary>
    public string OutputTopLevelFile { get; }

    /// <summary>
    /// Gets the source directory.
    /// </summary>
    public string SourceDirectory { get; }

    /// <summary>
    /// Gets the target directory.
    /// </summary>
    public string TargetDirectory { get; }

    /// <summary>
    /// Gets the archive type.
    /// </summary>
    public ArchiveType ArchiveType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PackOperationSettings"/> class.
    /// </summary>
    /// <param name="inputTopLevelDirectories">The input top level directories.</param>
    /// <param name="inputTopLevelFiles">The input top level files.</param>
    /// <param name="outputTopLevelFile">The output top level file.</param>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="targetDirectory">The target directory.</param>
    /// <param name="archiveType">The archive type.</param>
    public PackOperationSettings(
        IReadOnlyList<string> inputTopLevelDirectories,
        IReadOnlyList<string> inputTopLevelFiles,
        string outputTopLevelFile,
        string sourceDirectory,
        string targetDirectory,
        ArchiveType archiveType)
    {
        this.InputTopLevelDirectories = inputTopLevelDirectories;
        this.InputTopLevelFiles = inputTopLevelFiles;
        this.OutputTopLevelFile = outputTopLevelFile;
        this.SourceDirectory = sourceDirectory;
        this.TargetDirectory = targetDirectory;
        this.ArchiveType = archiveType;
    }
}