using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs.Results;
using RocketBlend.Services.Abstractions.Builds;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.ViewModels.Dialogs;

/// <summary>
/// The select builds dialog view model.
/// </summary>
public class SelectBuildsDialogViewModel : DialogViewModelBase<SelectBuildsDialogResult>, ISelectBuildsDialogViewModel, IDisposable
{
    private readonly IDisposable _cleanUp;
    private readonly IBlenderBuildService _blenderBuildService;
    private readonly ReadOnlyObservableCollection<BlenderBuildModel> _builds;
    private readonly ObservableAsPropertyHelper<bool> _isBusy;

    private bool _disposedValue;

    /// <inheritdoc />
    public ReadOnlyObservableCollection<BlenderBuildModel> Builds => this._builds;
    
    /// <inheritdoc />
    public ObservableCollection<BlenderBuildModel> SelectedBuilds { get; set; } = new();

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

        this.RefreshCommand = ReactiveCommand.CreateFromTask(this.RefreshBuilds);
        this.InstallBuildsCommand = ReactiveCommand.Create(this.InstallBuilds);

        var builds = this._blenderBuildService.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out this._builds)
            .DisposeMany()
            .Subscribe();

        this._isBusy = this.RefreshCommand
            .IsExecuting
            .ToProperty(this, x => x.IsBusy);

        this._cleanUp = new CompositeDisposable(builds);
    }

    /// <summary>
    /// Refreshes the builds.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task RefreshBuilds()
    {
        await this._blenderBuildService.Refresh().ConfigureAwait(false);
    }

    /// <summary>
    /// Installs the builds.
    /// </summary>
    private void InstallBuilds() => this.Close(new SelectBuildsDialogResult(this.SelectedBuilds.ToList()));

    #region IDisposable
    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                this._cleanUp.Dispose();
            }

            this._disposedValue = true;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}