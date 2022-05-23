using System.Reflection;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using RocketBlend.Core;
using RocketBlend.Core.Services.Interfaces;
using RocketBlend.DependencyInjection;
using Splat;

namespace RocketBlend;

/// <summary>
/// The application configurator.
/// </summary>
public static class ApplicationConfigurator
{
    /// <summary>
    /// Configures the service provider.
    /// </summary>
    public static void ConfigureServiceMSProvider()
    {
        MicrosoftBootstrapper.ConfigureServiceProvider();
    }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="app">The app.</param>
    public static void ConfigureApplicationServices(IMutableDependencyResolver services, IApplication app)
    {


        services.AddApplication(app);
        services.AddApplicationInfo();

        services.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        services.UseReactiveUI();
    }

    /// <summary>
    /// Configures the.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void Configure(IReadonlyDependencyResolver services)
    {
        services.ConfigureDatabase();
    }
}
