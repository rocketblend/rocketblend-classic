using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.Interfaces.Main.Installs;

/// <summary>
/// The install list view model interface.
/// </summary>
public interface IInstallListViewModel : IRoutableViewModel
{
    /// <summary>
    /// Gets the selected install.
    /// </summary>
    public BlenderBuildModel? SelectedInstall { get; }

    /// <summary>
    /// Gets the installs.
    /// </summary>
    public ReadOnlyObservableCollection<IInstallViewModel> Installs { get; }

    /// <summary>
    /// Gets the select builds command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> SelectBuildsCommand { get; }
}
