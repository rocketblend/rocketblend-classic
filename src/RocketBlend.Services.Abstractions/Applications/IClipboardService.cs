namespace RocketBlend.Services.Abstractions.Applications;

/// <summary>
/// The clipboard service interface.
/// </summary>
public interface IClipboardService
{
    /// <summary>
    /// Gets the text async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task<string> GetTextAsync();

    /// <summary>
    /// Gets the files async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task<IReadOnlyList<string>> GetFilesAsync();

    /// <summary>
    /// Sets the text async.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>A Task.</returns>
    Task SetTextAsync(string text);

    /// <summary>
    /// Sets the files async.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <returns>A Task.</returns>
    Task SetFilesAsync(IReadOnlyList<string> files);
}
