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
    Task<string> GetDirectoryAsync(string? initialDirectory = null);

    /// <summary>
    /// Gets the file async.
    /// </summary>
    /// <param name="initialFile">The initial file.</param>
    /// <returns>A Task.</returns>
    Task<string> GetFileAsync(string? initialFile = null);
}
