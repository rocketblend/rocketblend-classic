using System.IO;
using RocketBlend.Presentation.Avalonia.Configuration;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Services.Environment.Enums;
using RocketBlend.Services.Environment.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;
using Splat;

namespace RocketBlend.Presentation.Avalonia.DependencyInjection;

/// <summary>
/// The logging bootstrapper.
/// </summary>
public static class LoggingBootstrapper
{
    /// <summary>
    /// Registers the logging.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="resolver">The resolver.</param>
    public static void RegisterLogging(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton(() =>
        {
            var config = resolver.GetRequiredService<LoggingConfiguration>();
            var logFilePath = GetLogFileName(config, resolver);
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.MinimumLevel.Override("Default", config.DefaultLogLevel)
                //.MinimumLevel.Override("Microsoft", config.MicrosoftLogLevel)
                .WriteTo.Console()
                .WriteTo.File(logFilePath, fileSizeLimitBytes: config.LimitBytes)
                .CreateLogger();
            var factory = new SerilogLoggerFactory(logger);

            return factory.CreateLogger("Default");
        });
    }

    /// <summary>
    /// Gets the log file name.
    /// </summary>
    /// <param name="config">The config.</param>
    /// <param name="resolver">The resolver.</param>
    /// <returns>A string.</returns>
    private static string GetLogFileName(LoggingConfiguration config,
        IReadonlyDependencyResolver resolver)
    {
        var platformService = resolver.GetRequiredService<IPlatformService>();

        string logDirectory;
        if (platformService.GetPlatform() == Platform.Linux)
        {
            var environmentService = resolver.GetRequiredService<IEnvironmentService>();

            logDirectory = $"{environmentService.GetEnvironmentVariable("HOME")}/.config/rocketblend/logs";
        }
        else
        {
            logDirectory = Directory.GetCurrentDirectory();
        }

        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        return Path.Combine(logDirectory, config.LogFileName);
    }
}