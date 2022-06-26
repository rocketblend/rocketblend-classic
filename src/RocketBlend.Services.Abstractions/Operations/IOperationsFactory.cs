using RocketBlend.Services.Abstractions.Models.Operations;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The operations factory.
/// </summary>
public interface IOperationsFactory
{
    /// <summary>
    /// Creates the copy operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateCopyOperation(BinaryFileSystemOperationSettings settings);

    /// <summary>
    /// Creates the move operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateMoveOperation(BinaryFileSystemOperationSettings settings);

    /// <summary>
    /// Creates the delete operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateDeleteOperation(UnaryFileSystemOperationSettings settings);

    /// <summary>
    /// Creates the pack operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreatePackOperation(PackOperationSettings settings);

    /// <summary>
    /// Creates the extract operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateExtractOperation(ExtractArchiveOperationSettings settings);

    /// <summary>
    /// Creates the download operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateDownloadOperation(DownloadOperationSettings settings);

    /// <summary>
    /// Creates the install blender operation.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <returns>An IOperation.</returns>
    IOperation CreateInstallBlenderOperation(InstallBlenderOperationSettings settings);
}