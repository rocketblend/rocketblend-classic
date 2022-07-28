using System.Reactive;
using ReactiveUI;

namespace RocketBlend.Presentation.Interfaces.Menu;

/// <summary>
/// The menu view model interface.
/// </summary>
public interface IMenuViewModel
{
    /// <summary>
    /// Gets the exit command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> ExitCommand { get; }

    /// <summary>
    /// Gets the about command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> AboutCommand { get; }
}