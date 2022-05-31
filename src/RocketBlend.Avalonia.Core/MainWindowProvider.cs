using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using RocketBlend.Avalonia.Interfaces;

namespace RocketBlend.Avalonia.Core;

/// <summary>
/// The main window provider.
/// </summary>
public class MainWindowProvider : IMainWindowProvider
{
    /// <inheritdoc />
    public Window GetMainWindow()
    {
        var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        return lifetime.MainWindow;
    }
}
