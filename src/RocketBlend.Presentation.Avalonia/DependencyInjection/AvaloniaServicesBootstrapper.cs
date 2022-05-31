using RocketBlend.Avalonia.Core;
using RocketBlend.Avalonia.Interfaces;
using Splat;

namespace RocketBlend.Presentation.Avalonia.DependencyInjection;

/// <summary>
/// The avalonia services bootstrapper.
/// </summary>
public static class AvaloniaServicesBootstrapper
{
    /// <summary>
    /// Registers the avalonia services.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void RegisterAvaloniaServices(IMutableDependencyResolver services)
    {
        services.RegisterLazySingleton<IClipboardService>(() => new ClipboardService());
        services.RegisterLazySingleton<IMainWindowProvider>(() => new MainWindowProvider());
    }
}