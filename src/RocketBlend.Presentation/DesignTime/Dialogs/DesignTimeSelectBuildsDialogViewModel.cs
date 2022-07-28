using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.DesignTime.Dialogs;

/// <summary>
/// The design time select builds dialog view model.
/// </summary>
public class DesignTimeSelectBuildsDialogViewModel : DialogViewModelBase, ISelectBuildsDialogViewModel
{
    /// <summary>
    /// The number of builds.
    /// </summary>
    private const int NumberOfBuilds = 10;

    /// <inheritdoc />
    public bool IsBusy => true;

    /// <inheritdoc />
    public ReadOnlyObservableCollection<BlenderBuildModel> Builds { get; }

    /// <inheritdoc />
    public ObservableCollection<BlenderBuildModel> SelectedBuilds { get; set; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> RefreshCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> InstallBuildsCommand => ReactiveCommand.Create(() => { });

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeSelectBuildsDialogViewModel"/> class.
    /// </summary>
    public DesignTimeSelectBuildsDialogViewModel()
    {
        var builds = new ObservableCollection<BlenderBuildModel>();

        for (int i = 0; i < NumberOfBuilds; i++)
        {
            builds.Add(GenerateBuild());
        }

        this.Builds = new(builds);
        this.SelectedBuilds = new();
    }

    /// <summary>
    /// Generates the build.
    /// </summary>
    /// <returns>A BlenderBuildModel.</returns>
    private static BlenderBuildModel GenerateBuild()
    {
        return new BlenderBuildModel()
        {
            Name = "Build Name",
            Hash = "aaabbbcccdddeee",
            Tag = "Release",
        };
    }
}