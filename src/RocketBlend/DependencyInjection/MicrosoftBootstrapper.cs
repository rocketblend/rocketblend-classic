using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using RocketBlend.Application;
using RocketBlend.Application.ConfigurationOptions;
using RocketBlend.Blender;
using RocketBlend.Blender.Interfaces;
using RocketBlend.Core;
using RocketBlend.Infrastructure;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace RocketBlend.DependencyInjection;

/// <summary>
/// Bootstrapping.
/// </summary>
public static class MicrosoftBootstrapper
{
    /// <summary>
    /// Configures the service provider.
    /// </summary>
    public static void ConfigureServiceProvider()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var services = ConfigureServices(configuration);
        services.BuildServiceProvider();
    }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>An IServiceCollection.</returns>
    private static IServiceCollection ConfigureServices(IConfiguration configuration)
    {
        var services = new ServiceCollection();

        var settingsConfig = new SettingsConfig();
        configuration.Bind(settingsConfig);

        services.AddSingleton(settingsConfig);

        services.AddLogging();
        services.AddApplicationServices();
        services.AddInfrastructure(settingsConfig);

        // services.AddSingleton<IViewLocator, ConventionalViewLocator>();

        services.AddScoped<IBlenderClient, BlenderClient>();

        services.UseMicrosoftDependencyResolver();

        return services;
    }
}