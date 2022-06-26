using System.Reactive;
using DynamicData.Binding;
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
    public ObservableCollectionExtended<BlenderBuildModel> Builds { get; }

    /// <inheritdoc />
    public IEnumerable<BlenderBuildModel> SelectedBuilds => new List<BlenderBuildModel>();

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> RefreshCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> InstallBuildsCommand => ReactiveCommand.Create(() => { });

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeSelectBuildsDialogViewModel"/> class.
    /// </summary>
    public DesignTimeSelectBuildsDialogViewModel()
    {
        this.Builds = new();

        for(int i = 0; i < NumberOfBuilds; i++)
        {
            this.Builds.Add(GenerateBuild());
        }

        this.Builds.Add(GenerateBuild());
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
            Tag = "Release"
        };
    }
}
