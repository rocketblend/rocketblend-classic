using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Interfaces.Menu;

namespace RocketBlend.Presentation.Interfaces;

/// <summary>
/// The main window view model.
/// </summary>
public interface IMainWindowViewModel : IScreen
{
    /// <summary>
    /// Gets or sets the greeting.
    /// </summary>
    public string Greeting { get; }

    /// <summary>
    /// Gets the menu view model.
    /// </summary>
    public IMenuViewModel MenuViewModel { get; }
}
