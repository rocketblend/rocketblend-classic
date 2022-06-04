using RocketBlend.Presentation.Avalonia.DependencyInjection;
using Splat;

namespace RocketBlend.Presentation.Avalonia;

/// <summary>
/// The bootstrapper.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Registers the.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        EnvironmentServicesBootstrapper.RegisterEnvironmentServices(services, resolver);
        ConfigurationBootstrapper.RegisterConfiguration(services, resolver);
        LoggingBootstrapper.RegisterLogging(services, resolver);
        AvaloniaServicesBootstrapper.RegisterAvaloniaServices(services);
        ServicesBootstrapper.RegisterServices(services, resolver);
        ViewModelsBootstrapper.RegisterViewModels(services, resolver);
    }
}