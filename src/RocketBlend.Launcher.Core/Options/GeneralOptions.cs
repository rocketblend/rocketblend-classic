using RocketBlend.Launcher.Core.Options.Interfaces;

namespace RocketBlend.Launcher.Core.Options;

/// <summary>
/// The general options.
/// </summary>
public class GeneralOptions : IGeneralOptions
{
    /// <inheritdoc />
    public bool StartInBackground { get; set; }
}