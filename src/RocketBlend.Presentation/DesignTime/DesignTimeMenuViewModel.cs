using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Menu;

namespace RocketBlend.Presentation.DesignTime;

/// <summary>
/// The design time menu view model.
/// </summary>
public class DesignTimeMenuViewModel : IMenuViewModel
{
    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> ExitCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> AboutCommand => ReactiveCommand.Create(() => { });
}