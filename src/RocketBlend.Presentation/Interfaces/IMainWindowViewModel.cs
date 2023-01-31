using System.Reactive;
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
    /// Gets the tab index.
    /// </summary>
    int SelectedTabIndex { get; }

    /// <summary>
    /// Gets the menu view model.
    /// </summary>
    IMenuViewModel MenuViewModel { get; }

    /// <summary>
    /// Gets the operations view model.
    /// </summary>
    IOperationsViewModel OperationsViewModel { get; }

    /// <summary>
    /// Gets the operations state view model.
    /// </summary>
    IOperationsStateViewModel OperationsStateViewModel { get; }

    /// <summary>
    /// Gets the pipe command.
    /// </summary>
    ReactiveCommand<string, Unit> PipeCommand { get; }
}