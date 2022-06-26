using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Operations;

namespace RocketBlend.Presentation.DesignTime.Main.Operations;

/// <summary>
/// The design time operations view model.
/// </summary>
public class DesignTimeOperationsViewModel : IOperationsViewModel
{
    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> TestCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> OpenInDefaultCommand => ReactiveCommand.Create(() => { });
}
