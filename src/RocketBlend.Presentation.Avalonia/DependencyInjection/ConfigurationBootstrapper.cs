using Microsoft.Extensions.Configuration;
using RocketBlend.Presentation.Avalonia.Configuration;
using RocketBlend.Presentation.Configuration;
using Splat;

namespace RocketBlend.Presentation.Avalonia.DependencyInjection;

/// <summary>
/// The configuration bootstrapper.
/// </summary>
public static class ConfigurationBootstrapper
{
    /// <summary>
    /// Registers the configuration.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    public static void RegisterConfiguration(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        var configuration = BuildConfiguration();

        RegisterAboutDialogConfiguration(services, configuration);
        RegisterLoggingConfiguration(services, configuration);
        RegisterLanguagesConfiguration(services, configuration);
        RegisterOperationsStatesConfiguration(services, configuration);
    }

    /// <summary>
    /// Builds the configuration.
    /// </summary>
    /// <returns>An IConfiguration.</returns>
    private static IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

    /// <summary>
    /// Registers the logging configuration.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    private static void RegisterLoggingConfiguration(IMutableDependencyResolver services, IConfiguration configuration)
    {
        var config = new LoggingConfiguration();
        configuration.GetSection("Logging").Bind(config);
        services.RegisterConstant(config);
    }

    /// <summary>
    /// Registers the languages configuration.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    private static void RegisterLanguagesConfiguration(IMutableDependencyResolver services, IConfiguration configuration)
    {
        var config = new LanguagesConfiguration();
        configuration.GetSection("Languages").Bind(config);
        services.RegisterConstant(config);
    }

    /// <summary>
    /// Registers the about dialog configuration.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    private static void RegisterAboutDialogConfiguration(IMutableDependencyResolver services,
        IConfiguration configuration)
    {
        var config = new AboutDialogConfiguration();
        configuration.GetSection("About").Bind(config);
        services.RegisterConstant(config);
    }

    /// <summary>
    /// Registers the operations states configuration.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    private static void RegisterOperationsStatesConfiguration(IMutableDependencyResolver services,
            IConfiguration configuration)
    {
        var config = new OperationsStatesConfiguration();
        configuration.GetSection("OperationsStates").Bind(config);
        services.RegisterConstant(config);
    }
}
