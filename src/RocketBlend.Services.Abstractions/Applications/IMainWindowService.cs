namespace RocketBlend.Services.Abstractions.Applications;

/// <summary>
/// The main window service.
/// </summary>
public interface IMainWindowService
{
    /// <summary>
    /// Shows the window.
    /// </summary>
    void Show();

    /// <summary>
    /// Hide the window.
    /// </summary>
    void Hide();

    /// <summary>
    /// Shutdowns the window.
    /// </summary>
    void Shutdown();
}