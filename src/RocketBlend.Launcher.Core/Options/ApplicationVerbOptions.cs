using CommandLine;
using RocketBlend.Launcher.Core.Options.Interfaces;

namespace RocketBlend.Launcher.Core.Options;

/// <summary>
/// The default verb options.
/// </summary>
[Verb("app", isDefault: true, HelpText = "Application commands")]
public class ApplicationVerbOptions : IGeneralOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether background.
    /// </summary>
    [Option('u', "update")]
    public bool Update { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether background.
    /// </summary>
    [Option('r', "reset")]
    public bool Reset { get; set; }

    /// <inheritdoc />
    public bool StartInBackground { get; set; }
}
