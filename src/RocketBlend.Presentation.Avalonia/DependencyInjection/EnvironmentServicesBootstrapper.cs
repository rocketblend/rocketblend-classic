using RocketBlend.Presentation.Extensions;
using RocketBlend.Services.Environment.Implementations;
using RocketBlend.Services.Environment.Interfaces;
using Splat;

namespace RocketBlend.Presentation.Avalonia.DependencyInjection;

/// <summary>
/// The environment services bootstrapper.
/// </summary>
public static class EnvironmentServicesBootstrapper
{
    /// <summary>
    /// Registers the environment services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    public static void RegisterEnvironmentServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        RegisterCommonServices(services);
        RegisterPlatformSpecificServices(services, resolver);
    }

    /// <summary>
    /// Registers the common services.
    /// </summary>
    /// <param name="services">The services.</param>
    private static void RegisterCommonServices(IMutableDependencyResolver services)
    {
        services.RegisterLazySingleton<IDateTimeProvider>(() => new DateTimeProvider());
        services.RegisterLazySingleton<IEnvironmentService>(() => new EnvironmentService());
        services.RegisterLazySingleton<IProcessService>(() => new ProcessService());
        services.RegisterLazySingleton<IEnvironmentFileService>(() => new EnvironmentFileService());
        services.RegisterLazySingleton<IEnvironmentDirectoryService>(() => new EnvironmentDirectoryService());
        services.RegisterLazySingleton<IEnvironmentDriveService>(() => new EnvironmentDriveService());
        services.RegisterLazySingleton<IEnvironmentPathService>(() => new EnvironmentPathService());
        services.RegisterLazySingleton<IRegexService>(() => new RegexService());
        services.Register<IPlatformService>(() => new PlatformService());
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

        services.RegisterLazySingleton<IEnvironmentFileService>(() =>
            new EnvironmentFileServiceWindowsDecorator(
                new EnvironmentFileService()
            )
        );
        services.RegisterLazySingleton<IEnvironmentDirectoryService>(() =>
            new EnvironmentDirectoryServiceWindowsDecorator(
                new EnvironmentDirectoryService()
            )
        );
    }
}
