using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Operations;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
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

    /// <summary>
    /// Gets the operations view model.
    /// </summary>
    public IOperationsViewModel OperationsViewModel { get; }

    /// <summary>
    /// Gets the operations state view model.
    /// </summary>
    public IOperationsStateViewModel OperationsStateViewModel { get; }
}
