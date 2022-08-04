using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs.Results;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Presentation.ViewModels.Main.Installs;

/// <summary>
/// The install list view model.
/// </summary>
public class InstallListViewModel : ViewModelBase, IInstallListViewModel, IDisposable
{
    private readonly IDisposable _cleanUp;
    private readonly IDialogService _dialogService;
    private readonly IBlenderInstallService _blenderInstallService;
    private readonly IBlenderInstallStateService _blenderInstallStateService;
    private readonly IInstallViewModelFactory _installViewModelFactory;
    private readonly IBlenderInstallFactory _blenderInstallFactory;

    private readonly ReadOnlyObservableCollection<IInstallViewModel> _installs;

    private bool _disposedValue;

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IInstallViewModel> Installs => this._installs;

    /// <inheritdoc />
    [Reactive]
    public IInstallViewModel? SelectedInstall { get; set; }

    /// <inheritdoc />
    [Reactive]
    public bool ShowInstallPane { get; private set; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> SelectBuildsCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallListViewModel"/> class.
    /// </summary>
    /// <param name="screen">The screen.</param>
    public InstallListViewModel(
        IDialogService dialogService,
        IBlenderInstallService blenderInstallService,
        IBlenderInstallStateService blenderInstallStateService,
        IInstallViewModelFactory installViewModelFactory,
        IBlenderInstallFactory blenderInstallFactory,
        IScreen screen)
    {
        this._dialogService = dialogService;
        this._blenderInstallService = blenderInstallService;
        this._blenderInstallStateService = blenderInstallStateService;
        this._installViewModelFactory = installViewModelFactory;
        this._blenderInstallFactory = blenderInstallFactory;

        this.HostScreen = screen;

        this.SelectBuildsCommand = ReactiveCommand.CreateFromTask(this.ShowSelectBuildsDialogAsync);

        var installs = this._blenderInstallStateService.Connect()
            .Transform(this.CreateFrom)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out this._installs)
            .DisposeMany()
            .Subscribe();

        this.WhenAnyValue(x => x.SelectedInstall)
            .Subscribe(x => this.ShowInstallPane = x is not null);

        this._cleanUp = new CompositeDisposable(installs);
    }

    /// <summary>
    /// Shows the select builds dialog async.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task ShowSelectBuildsDialogAsync()
    {
        var result = await this._dialogService.ShowDialogAsync<SelectBuildsDialogResult>(nameof(SelectBuildsDialogViewModel));
        if (result is not null)
        {
            string path = GetRelativePath("installs/");
            foreach (var build in result.Builds)
            {
                var install = this._blenderInstallFactory.Create(build.Name, build.Tag, build.Hash, path, build.DownloadUrl);
                await this._blenderInstallService.CreateInstall(install);
                this.SelectInstallById(install.Id);
            }
        }
    }

    /// <summary>
    /// Selects the install by id.
    /// </summary>
    /// <param name="id">The id.</param>
    private void SelectInstallById(Guid id)
    {
        var install = this._installs.FirstOrDefault(x => x.Id == id);
        if (install is not null)
        {
            this.SelectedInstall = install;
        }
    }

    /// <summary>
    /// Creates the from.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>An IInstallViewModel.</returns>
    private IInstallViewModel CreateFrom(BlenderInstallModel model) => this._installViewModelFactory.Create(model);

    /// <summary>
    /// Gets the relative path.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <returns>A string.</returns>
    private static string GetRelativePath(string folder)
    {
        // Use path service.
        string workingPath = Directory.GetCurrentDirectory();
        string downloadPath = Path.Combine(workingPath, folder);
        return Directory.CreateDirectory(downloadPath).FullName; // Ensure path is created.
    }

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