using ReactiveUI;
using RocketBlend.Presentation.DesignTime.Main.Operations;
using RocketBlend.Presentation.DesignTime.Main.OperationsStates;
using RocketBlend.Presentation.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Operations;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Presentation.Interfaces.Menu;

namespace RocketBlend.Presentation.DesignTime;

/// <summary>
/// The design time main window view model.
/// </summary>
public class DesignTimeMainWindowViewModel : IMainWindowViewModel
{
    /// <inheritdoc />
    public int SelectedTabIndex => 0;

    /// <inheritdoc />
    public RoutingState Router => new();

    /// <inheritdoc />
    public IMenuViewModel MenuViewModel => new DesignTimeMenuViewModel();

    /// <inheritdoc />
    public IOperationsViewModel OperationsViewModel => new DesignTimeOperationsViewModel();

    /// <inheritdoc />
    public IOperationsStateViewModel OperationsStateViewModel => new DesignTimeOperationsStatesListViewModel();
}