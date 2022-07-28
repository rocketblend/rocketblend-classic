using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

namespace RocketBlend.Presentation.Interfaces.Main.Installs;

/// <summary>
/// The install list view model interface.
/// </summary>
public interface IInstallListViewModel : IRoutableViewModel
{
    /// <summary>
    /// Gets the selected install.
    /// </summary>
    IInstallViewModel? SelectedInstall { get; }

    /// <summary>
    /// Gets a value indicating whether show project pane.
    /// </summary>
    bool ShowInstallPane { get; }

    /// <summary>
    /// Gets the installs.
    /// </summary>
    ReadOnlyObservableCollection<IInstallViewModel> Installs { get; }

    /// <summary>
    /// Gets the select builds command.
    /// </summary>
    ReactiveCommand<Unit, Unit> SelectBuildsCommand { get; }
}