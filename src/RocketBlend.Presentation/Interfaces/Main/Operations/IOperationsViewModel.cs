using System.Reactive;
using ReactiveUI;

namespace RocketBlend.Presentation.Interfaces.Main.Operations;

/// <summary>
/// The operations view model.
/// </summary>
public interface IOperationsViewModel
{
    /// <summary>
    /// Gets the test command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> TestCommand { get; }

    /// <summary>
    /// Gets the open in default command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> OpenInDefaultCommand { get; }
}