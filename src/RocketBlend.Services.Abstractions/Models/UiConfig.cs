using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The UI config.
/// </summary>
public class UiConfig : ReactiveObject
{
    /// <summary>
    /// Gets or sets the window state.
    /// </summary>
    [Reactive]
    public string WindowState { get; set; } = "Normal";

    /// <summary>
    /// Gets or sets the window x.
    /// </summary>
    [Reactive]
    public int? WindowX { get; set; }

    /// <summary>
    /// Gets or sets the window y.
    /// </summary>
    [Reactive]
    public int? WindowY { get; set; }

    /// <summary>
    /// Gets or sets the window width.
    /// </summary>
    [Reactive]
    public double? WindowWidth { get; set; }

    /// <summary>
    /// Gets or sets the window height.
    /// </summary>
    [Reactive]
    public double? WindowHeight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether run on system startup.
    /// </summary>
    [Reactive]
    public bool RunOnSystemStartup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether hide on close.
    /// </summary>
    [Reactive]
    public bool HideOnClose { get; set; }
}