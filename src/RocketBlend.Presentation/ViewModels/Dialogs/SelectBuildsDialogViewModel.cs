using System.Reactive;
using System.Reactive.Concurrency;
using DynamicData.Binding;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs.Results;
using RocketBlend.Services.Abstractions.Builds;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.ViewModels.Dialogs;

/// <summary>
/// The select builds dialog view model.
/// </summary>
public class SelectBuildsDialogViewModel : DialogViewModelBase<SelectBuildsDialogResult>, ISelectBuildsDialogViewModel
{
    private readonly IBlenderBuildService _blenderBuildService;

    private readonly ObservableAsPropertyHelper<bool> _isBusy;

    /// <inheritdoc />
    public ObservableCollectionExtended<BlenderBuildModel> Builds { get; }

    /// <inheritdoc />
    public IEnumerable<BlenderBuildModel> SelectedBuilds { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> InstallBuildsCommand { get; }

    /// <inheritdoc />
    public bool IsBusy => this._isBusy.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectBuildsDialogViewModel"/> class.
    /// </summary>
    /// <param name="blenderBuildService">The blender build service.</param>
    public SelectBuildsDialogViewModel(IBlenderBuildService blenderBuildService)
    {
        this._blenderBuildService = blenderBuildService;

        this.Builds = new();
        this.SelectedBuilds = new List<BlenderBuildModel>();

        this.RefreshCommand = ReactiveCommand.CreateFromTask(this.RefreshBuilds);
        this.InstallBuildsCommand = ReactiveCommand.Create(this.InstallBuilds);

        RxApp.MainThreadScheduler.Schedule(this.LoadBuilds);

        this._isBusy = this.RefreshCommand
            .IsExecuting
            .ToProperty(this, x => x.IsBusy);
    }

    /// <summary>
    /// Loads the builds.
    /// </summary>
    private async void LoadBuilds()
    {
        await this._blenderBuildService.Initialize();
        this.SetBuilds();
    }

    /// <summary>
    /// Refreshes the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task RefreshBuilds()
    {
        await this._blenderBuildService.Refresh();
        this.SetBuilds();
    }

    /// <summary>
    /// Sets the builds.
    /// </summary>
    private void SetBuilds()
    {
        this.Builds.Clear();
        this.Builds.AddRange(this._blenderBuildService.BlenderBuilds);
    }

    /// <summary>
    /// Installs the builds.
    /// </summary>
    private void InstallBuilds() => this.Close(new SelectBuildsDialogResult(this.SelectedBuilds.ToList()));
}
