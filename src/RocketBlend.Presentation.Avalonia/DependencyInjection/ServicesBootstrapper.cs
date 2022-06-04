using System;
using RocketBlend.Avalonia.Interfaces;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Avalonia.Services.Implementations;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Services.Environment.Enums;
using RocketBlend.Services.Environment.Interfaces;
using Splat;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services;
using RocketBlend.WebScraper.Blender.Core.Interfaces;
using RocketBlend.WebScraper.Blender;

namespace RocketBlend.Presentation.Avalonia.DependencyInjection;

/// <summary>
/// The services bootstrapper.
/// </summary>
public static class ServicesBootstrapper
{
    /// <summary>
    /// Registers the services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    public static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        RegisterCommonServices(services, resolver);
        RegisterPlatformSpecificServices(services, resolver);
    }

    /// <summary>
    /// Registers the common services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterCommonServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton<IDialogService>(() => new DialogService(
            resolver.GetRequiredService<IMainWindowProvider>()
        ));
        services.RegisterLazySingleton<ISystemDialogService>(() => new SystemDialogService(
            resolver.GetRequiredService<IMainWindowProvider>()
        ));

        services.RegisterLazySingleton<IBlenderBuildScraperService>(() => new BlenderBuildScraperService());

        services.RegisterLazySingleton<IBlenderBuildService>(() => new BlenderBuildService(
            resolver.GetRequiredService<IBlenderBuildScraperService>()
        ));
    }

    /// <summary>
    /// Registers the platform specific services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterPlatformSpecificServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        var platformService = resolver.GetRequiredService<IPlatformService>();
        var platform = platformService.GetPlatform();

        switch (platform)
        {
            case Platform.Linux:
                RegisterLinuxServices(services, resolver);
                break;
            case Platform.MacOs:
                RegisterMacServices(services, resolver);
                break;
            case Platform.Windows:
                RegisterWindowsServices(services, resolver);
                break;
            case Platform.Unknown:
                throw new InvalidOperationException("Unsupported platform");
            default:
                throw new ArgumentOutOfRangeException(nameof(platform));
        }
    }

    /// <summary>
    /// Registers the linux services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterLinuxServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
    }

    /// <summary>
    /// Registers the mac services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterMacServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
    }

    /// <summary>
    /// Registers the windows services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterWindowsServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
    }
}