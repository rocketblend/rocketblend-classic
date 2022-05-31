using ReactiveUI;
using RocketBlend.Presentation.Interfaces;

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
}
