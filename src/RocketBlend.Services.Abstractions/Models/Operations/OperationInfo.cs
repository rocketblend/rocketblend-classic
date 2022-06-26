using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models.Operations;

/// <summary>
/// The operation info.
/// </summary>
public class OperationInfo
{
    /// <summary>
    /// Gets the operation type.
    /// </summary>
    public OperationType OperationType { get; }

    /// <summary>
    /// Gets the files.
    /// </summary>
    public IReadOnlyList<string> Files { get; }

    /// <summary>
    /// Gets the directories.
    /// </summary>
    public IReadOnlyList<string> Directories { get; }

    /// <summary>
    /// Gets the total files count.
    /// </summary>
    public int TotalFilesCount { get; }

    /// <summary>
    /// Gets the source directory.
    /// </summary>
    public string SourceDirectory { get; } = string.Empty;

    /// <summary>
    /// Gets the target directory.
    /// </summary>
    public string TargetDirectory { get; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class.
    /// </summary>
    /// <param name="operationType">The operation type.</param>
    /// <param name="settings">The settings.</param>
    public OperationInfo(OperationType operationType, BinaryFileSystemOperationSettings settings)
    {
        this.OperationType = operationType;
        this.Files = settings.InputTopLevelFiles;
        this.Directories = settings.InputTopLevelDirectories;
        this.TotalFilesCount = settings.FilesDictionary.Count;
        this.SourceDirectory = settings.SourceDirectory;
        this.TargetDirectory = settings.TargetDirectory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class.
    /// </summary>
    /// <param name="operationType">The operation type.</param>
    /// <param name="settings">The settings.</param>
    public OperationInfo(OperationType operationType, UnaryFileSystemOperationSettings settings)
    {
        this.OperationType = operationType;
        this.Files = settings.TopLevelFiles;
        this.Directories = settings.TopLevelDirectories;
        this.TotalFilesCount = this.Files.Count + this.Directories.Count;
        this.SourceDirectory = settings.SourceDirectory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public OperationInfo(ExtractArchiveOperationSettings settings)
    {
        this.OperationType = OperationType.Extract;
        this.Files = new[] { settings.InputTopLevelFile };
        this.Directories = Array.Empty<string>();
        this.TotalFilesCount = 1;
        this.TargetDirectory = settings.TargetDirectory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public OperationInfo(PackOperationSettings settings)
    {
        this.OperationType = OperationType.Pack;
        this.Files = settings.InputTopLevelFiles;
        this.Directories = settings.InputTopLevelDirectories;
        this.TotalFilesCount = this.Files.Count;
        this.TargetDirectory = settings.TargetDirectory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public OperationInfo(DownloadOperationSettings settings)
    {
        this.Files = new[] { settings.SourceUri.ToString() };
        this.OperationType = OperationType.Download;
        this.Directories = Array.Empty<string>();
        this.TargetDirectory = settings.TargetDirectory;
        this.TotalFilesCount = 1;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="OperationInfo"/> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public OperationInfo(InstallBlenderOperationSettings settings)
    {
        this.Files = new[] { settings.SourceUri.ToString() };
        this.OperationType = OperationType.InstallBlender;
        this.TargetDirectory = settings.TargetDirectory;
        this.Directories = Array.Empty<string>();
        this.TotalFilesCount = 1;
    }
}