using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using RocketBlend.Services.Avalonia.Interfaces;

namespace RocketBlend.Services.Avalonia.Implementations;

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
