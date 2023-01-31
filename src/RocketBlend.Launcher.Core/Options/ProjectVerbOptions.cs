using CommandLine;
using RocketBlend.Launcher.Core.Options.Interfaces;

namespace RocketBlend.Launcher.Core.Options;

/// <summary>
/// The project verb options.
/// </summary>
[Verb("project", HelpText = "Project commands")]
public class ProjectVerbOptions : IGeneralOptions
{
    /// <summary>
    /// Gets or sets the open path.
    /// </summary>
    [Option('o', "open")]
    public string? OpenPath { get; set; }

    /// <summary>
    /// Gets or sets the project path.
    /// </summary>
    [Option('c', "create")]
    public string? CreationPath { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether status.
    /// </summary>
    [Option('s', "status")]
    public bool Status { get; set; }

    /// <inheritdoc />
    public bool StartInBackground { get; set; }
}
