using System.Reflection;
using ReactiveUI;
using RocketBlend.Presentation.Avalonia.Views.Dialogs;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Interfaces.Menu;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Dialogs;
using RocketBlend.Presentation.ViewModels.Main.Installs;
using RocketBlend.Presentation.ViewModels.Menu;
using RocketBlend.Presentation.Views.Dialogs;
using RocketBlend.Services.Abstractions;
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
        services.RegisterLazySingleton<IMenuViewModel>(() => new MenuViewModel());
        services.RegisterLazySingleton<IInstallListViewModel>(() => new InstallListViewModel(
            resolver.GetRequiredService<IDialogService>(),
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
