using System;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Services;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Abstractions.Builds;
using RocketBlend.Services.Abstractions.Images;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Operations;
using RocketBlend.Services.Abstractions.Projects;
using RocketBlend.Services.Archive;
using RocketBlend.Services.Archives;
using RocketBlend.Services.Avalonia.Implementations.Dialogs;
using RocketBlend.Services.Avalonia.Interfaces;
using RocketBlend.Services.Builds;
using RocketBlend.Services.Configuration;
using RocketBlend.Services.Environment.Enums;
using RocketBlend.Services.Environment.Interfaces;
using RocketBlend.Services.Images;
using RocketBlend.Services.Installs;
using RocketBlend.Services.Operations;
using RocketBlend.Services.Projects;
using RocketBlend.Services.Windows;
using RocketBlend.WebScraper.Blender;
using RocketBlend.WebScraper.Blender.Core.Interfaces;
using Splat;

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
        services.RegisterLazySingleton<IColorService>(() => new ColorService());
        services.RegisterLazySingleton<IDialogService>(() => new DialogService(
            resolver.GetRequiredService<IMainWindowProvider>()
        ));
        services.RegisterLazySingleton<ISystemDialogService>(() => new SystemDialogService(
            resolver.GetRequiredService<IMainWindowProvider>()
        ));

        services.RegisterLazySingleton<IArchiveProcessorFactory>(() => new ArchiveProcessorFactory(
            resolver.GetRequiredService<IFileService>(),
            resolver.GetRequiredService<IDirectoryService>(),
            resolver.GetRequiredService<IFileNameGenerationService>(),
            resolver.GetRequiredService<IPathService>()
        ));

        services.RegisterLazySingleton<IArchiveTypeMapper>(() => new ArchiveTypeMapper(
            resolver.GetRequiredService<IPathService>(),
            resolver.GetRequiredService<ArchiveTypeMapperConfiguration>()
        ));
        services.RegisterLazySingleton<INodeService>(() => new NodeService(
            resolver.GetRequiredService<IFileService>(),
            resolver.GetRequiredService<IDirectoryService>()
        ));

        services.RegisterLazySingleton<IArchiveService>(() => new ArchiveService(
            resolver.GetRequiredService<IArchiveTypeMapper>(),
            resolver.GetRequiredService<IPathService>(),
            resolver.GetRequiredService<IOperationsService>(),
            resolver.GetRequiredService<IFileNameGenerationService>()
        ));

        services.RegisterLazySingleton<IFileService>(() => new FileService(
            resolver.GetRequiredService<IPathService>(),
            resolver.GetRequiredService<IEnvironmentFileService>(),
            resolver.GetRequiredService<Microsoft.Extensions.Logging.ILogger>()
        ));

        services.RegisterLazySingleton<IOperationsFactory>(() => new OperationsFactory(
            resolver.GetRequiredService<IDirectoryService>(),
            resolver.GetRequiredService<IFileService>(),
            resolver.GetRequiredService<IPathService>(),
            resolver.GetRequiredService<IFileNameGenerationService>(),
            resolver.GetRequiredService<Microsoft.Extensions.Logging.ILogger>(),
            resolver.GetRequiredService<IArchiveProcessorFactory>()
        ));

        // services.RegisterLazySingleton<INodesSelectionService>(() => new NodesSelectionService());
        services.RegisterLazySingleton<IOperationsService>(() => new OperationsService(
            resolver.GetRequiredService<IOperationsFactory>(),
            resolver.GetRequiredService<IDirectoryService>(),
            resolver.GetRequiredService<IResourceOpeningService>(),
            resolver.GetRequiredService<IFileService>(),
            resolver.GetRequiredService<IPathService>(),
            resolver.GetRequiredService<IOperationsStateService>()
        ));

        services.RegisterLazySingleton<IDirectoryService>(() => new DirectoryService(
            resolver.GetRequiredService<IPathService>(),
            resolver.GetRequiredService<IEnvironmentDirectoryService>(),
            resolver.GetRequiredService<IEnvironmentFileService>(),
            resolver.GetRequiredService<Microsoft.Extensions.Logging.ILogger>()
        ));

        services.RegisterLazySingleton<IPathService>(() => new PathService(
            resolver.GetRequiredService<IEnvironmentPathService>()
        ));

        services.RegisterLazySingleton<IOperationsStateService>(() => new OperationsStateService());
        services.RegisterLazySingleton<IFileNameGenerationService>(() => new FileNameGenerationService(
            resolver.GetRequiredService<INodeService>(),
            resolver.GetRequiredService<IPathService>()
        ));

        services.RegisterLazySingleton<IImageProcessingService>(() => new ImageProcessingService(
            resolver.GetRequiredService<IFileService>()
        ));

        services.RegisterLazySingleton<IImageService>(() => new ImageService(
            resolver.GetRequiredService<IImageProcessingService>(),
            resolver.GetRequiredService<IDirectoryService>(),
            resolver.GetRequiredService<IFileService>(),
            resolver.GetRequiredService<IFileNameGenerationService>(),
            resolver.GetRequiredService<IPathService>()
        ));

        services.RegisterLazySingleton<IBlenderService>(() => new BlenderService(
            resolver.GetRequiredService<IResourceOpeningService>(),
            resolver.GetRequiredService<IFileService>()
        ));

        services.RegisterLazySingleton<IBlenderBuildScraperService>(() => new BlenderBuildScraperService());

        services.RegisterLazySingleton<IBlenderBuildService>(() => new BlenderBuildService(
            resolver.GetRequiredService<IBlenderBuildScraperService>()
        ));

        services.RegisterLazySingleton<IBlenderInstallStateService>(() => new BlenderInstallStateService());
        services.RegisterLazySingleton<IBlenderInstallFactory>(() => new BlenderInstallFactory(
            resolver.GetRequiredService<IColorService>()
        ));
        services.RegisterLazySingleton<IBlenderInstallService>(() => new BlenderInstallService(
            resolver.GetRequiredService<IBlenderInstallStateService>()
        ));

        services.RegisterLazySingleton<IProjectStateService>(() => new ProjectStateService());
        services.RegisterLazySingleton<IProjectFactory>(() => new ProjectFactory(
            resolver.GetRequiredService<IColorService>(),
            resolver.GetRequiredService<IImageService>()
        ));

        services.RegisterLazySingleton<IProjectService>(() => new ProjectService(
            resolver.GetRequiredService<IBlenderService>(),
            resolver.GetRequiredService<IProjectStateService>(),
            resolver.GetRequiredService<IBlenderInstallStateService>()
        ));

        services.RegisterLazySingleton<ILaunchArgumentService>(() => new LaunchArgumentService(
            resolver.GetRequiredService<IProjectService>()
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
        services.RegisterLazySingleton<IResourceOpeningService>(() => new WindowsResourceOpeningService(
            resolver.GetRequiredService<IProcessService>()
        ));
    }
}