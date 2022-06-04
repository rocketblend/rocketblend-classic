using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using RocketBlend.Presentation.Services.Interfaces;

namespace RocketBlend.Presentation.Avalonia.Services.Implementations;

/// <summary>
/// The application closer.
/// </summary>
public class ApplicationCloser : IApplicationCloser
{
    /// <inheritdoc />
    public void Shutdown()
    {
        var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        lifetime.Shutdown();
    }
}
