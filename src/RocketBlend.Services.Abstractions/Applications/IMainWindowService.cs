namespace RocketBlend.Services.Abstractions.Applications;

/// <summary>
/// The main window service.
/// </summary>
public interface IMainWindowService
{
    /// <summary>
    /// Shows the.
    /// </summary>
    void Show();

    /// <summary>
    /// Closes the.
    /// </summary>
    void Close();

    /// <summary>
    /// Shutdowns the.
    /// </summary>
    void Shutdown();
}