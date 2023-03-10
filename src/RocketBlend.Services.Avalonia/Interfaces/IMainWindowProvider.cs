using Avalonia.Controls;

namespace RocketBlend.Services.Avalonia.Interfaces;

/// <summary>
/// The main window provider interface.
/// </summary>
public interface IMainWindowProvider
{
    /// <summary>
    /// Gets the main window.
    /// </summary>
    /// <returns>A Window.</returns>
    Window GetMainWindow();
}