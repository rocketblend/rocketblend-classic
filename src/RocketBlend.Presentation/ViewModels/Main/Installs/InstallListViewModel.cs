using System.Collections.ObjectModel;
using System.Drawing;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs.Results;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.ViewModels.Main.Installs;

/// <summary>
/// The install list view model.
/// </summary>
public class InstallListViewModel : ViewModelBase, IInstallListViewModel, IDisposable
{
    private readonly IDisposable _cleanUp;
    private readonly IDialogService _dialogService;
    private readonly IBlenderInstallService _blenderInstallService;
    private readonly IDownloadService _downloadService;
    private readonly IInstallViewModelFactory _installViewModelFactory;
    private readonly Random _rnd = new();

    private readonly ReadOnlyObservableCollection<IInstallViewModel> _installs;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallListViewModel"/> class.
    /// </summary>
    /// <param name="screen">The screen.</param>
    public InstallListViewModel(
        IDialogService dialogService,
        IBlenderInstallService blenderInstallService,
        IDownloadService downloadService,
        IInstallViewModelFactory installViewModelFactory,
        IScreen screen)
    {
        this._dialogService = dialogService;
        this._blenderInstallService = blenderInstallService;
        this._downloadService = downloadService;
        this._installViewModelFactory = installViewModelFactory;

        this.HostScreen = screen;

        this.SelectBuildsCommand = ReactiveCommand.CreateFromTask(this.ShowSelectBuildsDialogAsync);

        var installs = this._blenderInstallService.Connect()
            .Transform(this.CreateFrom)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out this._installs)
            .DisposeMany()
            .Subscribe();

        this._cleanUp = new CompositeDisposable(installs);
    }

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IInstallViewModel> Installs => this._installs;

    /// <inheritdoc />
    public BlenderBuildModel? SelectedInstall { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> SelectBuildsCommand { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    /// <inheritdoc />
    public void Dispose() => this._cleanUp.Dispose();

    /// <summary>
    /// Shows the select builds dialog async.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task ShowSelectBuildsDialogAsync()
    {
        var result = await this._dialogService.ShowDialogAsync<SelectBuildsDialogResult>(nameof(SelectBuildsDialogViewModel));
        if(result is not null)
        {
            string path = GetRelativePath("installs/");
            foreach (var build in result.Builds)
            {
                var install = new BlenderInstallModel()
                {
                    Name = build.Name,
                    Tag = build.Tag,
                    Hash = build.Hash,
                    FullPath = Path.Combine(path, Path.GetFileNameWithoutExtension(build.DownloadUrl), "blender.exe"),
                    SourceUri = new Uri(build.DownloadUrl),
                    BackgroundColor = this.GenerateColour()
                };

                await this._blenderInstallService.AddOrUpdateInstall(install);
            }
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

    /// <summary>
    /// Generates the colour.
    /// </summary>
    /// <returns>A string.</returns>
    private string GenerateColour()
    {
        return ColorTranslator.ToHtml(Color.FromArgb(this._rnd.Next(256), this._rnd.Next(256), this._rnd.Next(256))).ToString();
    }
}
