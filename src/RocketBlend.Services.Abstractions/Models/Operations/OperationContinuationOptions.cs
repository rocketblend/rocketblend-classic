using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models.Operations;

/// <summary>
/// The operation continuation options.
/// </summary>
public class OperationContinuationOptions
{
    /// <summary>
    /// Gets the file path.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Gets a value indicating whether apply to all.
    /// </summary>
    public bool ApplyToAll { get; }

    /// <summary>
    /// Gets the mode.
    /// </summary>
    public OperationContinuationMode Mode { get; }

    /// <summary>
    /// Gets the new file path.
    /// </summary>
    public string NewFilePath { get; }

    /// <summary>
    /// Prevents a default instance of the <see cref="OperationContinuationOptions"/> class from being created.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="applyToAll">If true, apply to all.</param>
    /// <param name="mode">The mode.</param>
    /// <param name="newFilePath">The new file path.</param>
    private OperationContinuationOptions(
        string filePath,
        bool applyToAll,
        OperationContinuationMode mode,
        string newFilePath = "")
    {
        this.FilePath = filePath;
        this.ApplyToAll = applyToAll;
        this.Mode = mode;
        this.NewFilePath = newFilePath;
    }

    /// <summary>
    /// Creates the renaming continuation options.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="applyToAll">If true, apply to all.</param>
    /// <param name="newFilePath">The new file path.</param>
    /// <returns>An OperationContinuationOptions.</returns>
    public static OperationContinuationOptions CreateRenamingContinuationOptions(
        string filePath, bool applyToAll, string newFilePath) =>
        new(filePath, applyToAll, OperationContinuationMode.Rename, newFilePath);

    /// <summary>
    /// Creates the continuation options.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="applyToAll">If true, apply to all.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>An OperationContinuationOptions.</returns>
    public static OperationContinuationOptions CreateContinuationOptions(
        string filePath, bool applyToAll, OperationContinuationMode mode) =>
        new(filePath, applyToAll, mode);
}