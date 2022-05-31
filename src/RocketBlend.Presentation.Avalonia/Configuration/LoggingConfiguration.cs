using Serilog.Events;

namespace RocketBlend.Presentation.Avalonia.Configuration;

/// <summary>
/// The logging configuration.
/// </summary>
public class LoggingConfiguration
{
    /// <summary>
    /// Gets or sets the log file name.
    /// </summary>
    public string LogFileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the limit bytes.
    /// </summary>
    public long LimitBytes { get; set; }

    /// <summary>
    /// Gets or sets the default log level.
    /// </summary>
    public LogEventLevel DefaultLogLevel { get; set; }

    /// <summary>
    /// Gets or sets the Microsoft log level.
    /// </summary>
    public LogEventLevel MicrosoftLogLevel { get; set; }
}
