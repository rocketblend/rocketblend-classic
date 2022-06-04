using ReactiveUI;
using RocketBlend.Presentation.DesignTime.Main.Installs;
using RocketBlend.Presentation.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Interfaces.Menu;

namespace RocketBlend.Presentation.DesignTime;

/// <summary>
/// The design time main window view model.
/// </summary>
public class DesignTimeMainWindowViewModel : IMainWindowViewModel
{
    /// <inheritdoc />
    public string Greeting => "Greeting Text!";

    /// <inheritdoc />
    public RoutingState Router => new();

    /// <inheritdoc />
    public IMenuViewModel MenuViewModel => new DesignTimeMenuViewModel();
}
