using System.Reactive;
using DynamicData.Binding;
using ReactiveUI;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.Interfaces.Dialogs;

/// <summary>
/// The select builds dialogs view model interface.
/// </summary>
public interface ISelectBuildsDialogViewModel
{
    /// <summary>
    /// Gets a value indicating whether is busy.
    /// </summary>
    public bool IsBusy { get; }

    /// <summary>
    /// Gets the builds.
    /// </summary>
    public ObservableCollectionExtended<BlenderBuildModel> Builds { get; }

    /// <summary>
    /// Gets the selected builds.
    /// </summary>
    public IEnumerable<BlenderBuildModel> SelectedBuilds { get; }

    /// <summary>
    /// Gets the refresh command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    /// <summary>
    /// Gets the install builds.
    /// </summary>
    public ReactiveCommand<Unit, Unit> InstallBuildsCommand { get; }
}
