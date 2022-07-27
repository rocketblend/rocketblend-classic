using RocketBlend.Presentation.Services.Models;

namespace RocketBlend.Presentation.Services.Interfaces;

/// <summary>
/// The system dialog service interface.
/// </summary>
public interface ISystemDialogService
{
    /// <summary>
    /// Gets the directory async.
    /// </summary>
    /// <param name="initialDirectory">The initial directory.</param>
    /// <returns>A Task.</returns>
    Task<string?> GetDirectoryAsync(string? initialDirectory = null);

    /// <summary>
    /// Gets the file async.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="initialFile">The initial file.</param>
    /// <returns>A Task.</returns>
    Task<string?> GetFileAsync(IEnumerable<FileDialogFilter> filter, string? initialFile = null);

    /// <summary>
    /// Gets the file async.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="initialFile">The initial file.</param>
    /// <param name="allowMultiple">If true, allow multiple.</param>
    /// <returns>A Task.</returns>
    Task<IEnumerable<string?>> GetFilesAsync(IEnumerable<FileDialogFilter> filter, string? initialFile = null, bool allowMultiple = false);

    /// <summary>
    /// Saves the file async.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="initialFile">The initial file.</param>
    /// <param name="defaultExtension">The default extension.</param>
    /// <returns>A Task.</returns>
    Task<string?> SaveFileAsync(IEnumerable<FileDialogFilter> filter, string? initialFile = null, string? defaultExtension = null);
}
