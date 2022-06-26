using ReactiveUI;
using RocketBlend.Presentation.Configuration;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Factories.Implementations;
using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Interfaces.Main.Operations;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Presentation.Interfaces.Menu;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Dialogs;
using RocketBlend.Presentation.ViewModels.Main.Installs;
using RocketBlend.Presentation.ViewModels.Main.Operations;
using RocketBlend.Presentation.ViewModels.Main.OperationsStates;
using RocketBlend.Presentation.ViewModels.Menu;
using RocketBlend.Presentation.Views.Dialogs;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Operations;
using Splat;

namespace RocketBlend.Presentation.Avalonia.DependencyInjection;

/// <summary>
/// The view models bootstrapper.
/// </summary>
public class ViewModelsBootstrapper
{
    /// <summary>
    /// Registers the view models.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    public static void RegisterViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        RegisterCommonViewModels(services, resolver);
        RegisterViewLocator(services, resolver);
    }

    /// <summary>
    /// Registers the common view models.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterCommonViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton<IInstallViewModelFactory>(() => new InstallViewModelFactory(
            resolver.GetRequiredService<IBlenderInstallService>(),
            resolver.GetRequiredService<IOperationsService>()
        ));

        services.Register<IOperationsViewModel>(() => new OperationsViewModel(
            resolver.GetRequiredService<IOperationsService>(),
            resolver.GetRequiredService<IDialogService>(),
            resolver.GetRequiredService<IDirectoryService>()
        ));

        services.RegisterLazySingleton<IOperationStateViewModelFactory>(() => new OperationStateViewModelFactory(
            resolver.GetRequiredService<IPathService>()
        ));

        services.RegisterLazySingleton<IOperationsStateViewModel>(() => new OperationsStatesListViewModel(
            resolver.GetRequiredService<IOperationsStateService>(),
            resolver.GetRequiredService<IOperationStateViewModelFactory>(),
            resolver.GetRequiredService<OperationsStatesConfiguration>()
        ));

        services.RegisterLazySingleton<IMenuViewModel>(() => new MenuViewModel());
        services.RegisterLazySingleton<IInstallListViewModel>(() => new InstallListViewModel(
            resolver.GetRequiredService<IDialogService>(),
            resolver.GetRequiredService<IBlenderInstallService>(),
            resolver.GetRequiredService<IDownloadService>(),
            resolver.GetRequiredService<IInstallViewModelFactory>(),
            resolver.GetRequiredService<IScreen>()
        ));

        // Change to allow interface use for dialogs.
        services.Register(() => new AboutDialogViewModel());
        services.Register(() => new SelectBuildsDialogViewModel(
            resolver.GetRequiredService<IBlenderBuildService>()
        ));
    }

    /// <summary>
    /// Registers the view locator.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    private static void RegisterViewLocator(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton(() => new ConventionalViewLocator(), typeof(IViewLocator));
    }
}
