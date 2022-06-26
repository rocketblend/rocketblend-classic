namespace RocketBlend.Services.Abstractions.Models.Operations;

/// <summary>
/// The binary file system operation settings.
/// </summary>
public class BinaryFileSystemOperationSettings
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
    /// Gets the output top level directories.
    /// </summary>
    public IReadOnlyList<string> OutputTopLevelDirectories { get; }

    /// <summary>
    /// Gets the output top level files.
    /// </summary>
    public IReadOnlyList<string> OutputTopLevelFiles { get; }

    /// <summary>
    /// Gets the files dictionary.
    /// </summary>
    public IReadOnlyDictionary<string, string> FilesDictionary { get; }

    /// <summary>
    /// Gets the empty directories.
    /// </summary>
    public IReadOnlyList<string> EmptyDirectories { get; }

    /// <summary>
    /// Gets the source directory.
    /// </summary>
    public string SourceDirectory { get; } = string.Empty;

    /// <summary>
    /// Gets the target directory.
    /// </summary>
    public string TargetDirectory { get; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryFileSystemOperationSettings"/> class.
    /// </summary>
    /// <param name="inputTopLevelDirectories">The input top level directories.</param>
    /// <param name="inputTopLevelFiles">The input top level files.</param>
    /// <param name="outputTopLevelDirectories">The output top level directories.</param>
    /// <param name="outputTopLevelFiles">The output top level files.</param>
    /// <param name="filesDictionary">The files dictionary.</param>
    /// <param name="emptyDirectories">The empty directories.</param>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="targetDirectory">The target directory.</param>
    public BinaryFileSystemOperationSettings(
        IReadOnlyList<string> inputTopLevelDirectories,
        IReadOnlyList<string> inputTopLevelFiles,
        IReadOnlyList<string> outputTopLevelDirectories,
        IReadOnlyList<string> outputTopLevelFiles,
        IReadOnlyDictionary<string, string> filesDictionary,
        IReadOnlyList<string> emptyDirectories,
        string sourceDirectory = "",
        string targetDirectory = "")
    {
        this.InputTopLevelDirectories = inputTopLevelDirectories;
        this.InputTopLevelFiles = inputTopLevelFiles;
        this.OutputTopLevelDirectories = outputTopLevelDirectories;
        this.OutputTopLevelFiles = outputTopLevelFiles;
        this.FilesDictionary = filesDictionary;
        this.EmptyDirectories = emptyDirectories;
        this.SourceDirectory = sourceDirectory;
        this.TargetDirectory = targetDirectory;
    }
}