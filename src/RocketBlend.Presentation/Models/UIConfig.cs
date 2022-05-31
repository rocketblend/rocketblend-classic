using ReactiveUI.Fody.Helpers;

namespace RocketBlend.Presentation.Models;

/// <summary>
/// The UI config.
/// </summary>
public class UIConfig
{
    /// <summary>
    /// Gets or sets the window state.
    /// </summary>
    [Reactive] public string WindowState { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether run on system startup.
    /// </summary>
    [Reactive] public bool RunOnSystemStartup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether hide on close.
    /// </summary>
    [Reactive] public bool HideOnClose { get; set; }
}
