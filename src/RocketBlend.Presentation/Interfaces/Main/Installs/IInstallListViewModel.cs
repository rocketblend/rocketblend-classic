using System.Reactive;
using ReactiveUI;

namespace RocketBlend.Presentation.Interfaces.Main.Installs;

/// <summary>
/// The install list view model interface.
/// </summary>
public interface IInstallListViewModel : IRoutableViewModel
{
    /// <summary>
    /// Gets the select builds command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> SelectBuildsCommand { get; }
}
