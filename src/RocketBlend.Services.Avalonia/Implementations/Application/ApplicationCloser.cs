using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using RocketBlend.Services.Abstractions.Applications;

namespace RocketBlend.Services.Avalonia.Implementations.Applications;

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