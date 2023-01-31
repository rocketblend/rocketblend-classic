using CommandLine;

namespace RocketBlend.Launcher.Core.Options.Interfaces;

/// <summary>
/// The general options.
/// </summary>
public interface IGeneralOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether background.
    /// </summary>
    [Option('b', "background")]
    bool StartInBackground { get; set; }
}
