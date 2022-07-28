using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.Interfaces.Dialogs;

/// <summary>
/// The select builds dialogs view model interface.
/// </summary>
public interface ISelectBuildsDialogViewModel
{
    /// <summary>
    /// Gets the builds.
    /// </summary>
    ReadOnlyObservableCollection<BlenderBuildModel> Builds { get; }

    /// <summary>
    /// Gets the selected builds.
    /// </summary>
    ObservableCollection<BlenderBuildModel> SelectedBuilds { get; set; }

    /// <summary>
    /// Gets a value indicating whether is busy.
    /// </summary>
    bool IsBusy { get; }

    /// <summary>
    /// Gets the refresh command.
    /// </summary>
    ReactiveCommand<Unit, Unit> RefreshCommand { get; }
    
    /// <summary>
    /// Gets the install builds.
    /// </summary>
    ReactiveCommand<Unit, Unit> InstallBuildsCommand { get; }
}