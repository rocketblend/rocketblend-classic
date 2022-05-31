using ReactiveUI;

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
}
