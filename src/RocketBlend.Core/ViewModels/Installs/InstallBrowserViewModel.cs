using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using ReactiveUI;
using RocketBlend.Application.Commands.Builds;
using RocketBlend.Application.Commands.Installs;
using RocketBlend.Application.Queries.Builds.External;
using RocketBlend.Application.Queries.Installs;
using RocketBlend.WebScraper.Blender.Core.Enums;

namespace RocketBlend.Core.ViewModels.Installs;

/// <summary>
/// The build browser view model.
/// </summary>
public class InstallBrowserViewModel : ViewModelBase, IRoutableViewModel
{
    private InstallViewModel? _selectedInstall;

    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallBrowserViewModel"/> class.
    /// </summary>
    /// <param name="screen">The screen.</param>
    public InstallBrowserViewModel(IScreen screen)
    {
        this.HostScreen = screen;

        RxApp.MainThreadScheduler.Schedule(this.LoadInstalls);

        // this.RefreshAvailableBuilds();
        // this.CreateInstall();

        this.WhenActivated((CompositeDisposable disposables) =>
        {
        });
    }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    ///<inheritdoc />
    public ObservableCollection<InstallViewModel> Installs { get; } = new();

    /// <summary>
    /// Gets or sets the selected install.
    /// </summary>
    public InstallViewModel? SelectedInstall
    {
        get => this._selectedInstall;
        set => this.RaiseAndSetIfChanged(ref this._selectedInstall, value);
    }

    /// <summary>
    /// Loads the installs.
    /// </summary>
    private async void LoadInstalls()
    {
        this._cancellationTokenSource?.Cancel();
        this._cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = this._cancellationTokenSource.Token;

        var installs = await this.Mediator.Send(new GetInstallsQuery(), cancellationToken);

        foreach (var install in installs.Items)
        {
            this.Installs.Add(new InstallViewModel(install));
        }
    }

    /// <summary>
    /// Creates the install.
    /// </summary>
    private async void CreateInstall()
    {
        string buildId = "0C56D3DA-75FE-4C80-AFA9-0AE423046B11";

        await this.Mediator.Send(new CreateInstallCommand(
            Guid.NewGuid(),
            Guid.Parse(buildId),
            string.Empty));

        buildId = "EDAE4538-C780-4578-967B-39FD2CD43A64";
        await this.Mediator.Send(new CreateInstallCommand(
            Guid.NewGuid(),
            Guid.Parse(buildId),
            string.Empty));

        buildId = "0E4DE34E-2E37-4AE6-B3F1-F1FDACB85ACB";
        await this.Mediator.Send(new CreateInstallCommand(
            Guid.NewGuid(),
            Guid.Parse(buildId),
            string.Empty));
    }

    /// <summary>
    /// Loads the builds.
    /// </summary>
    private void LoadBuilds()
    {
        return;
    }

    /// <summary>
    /// Refreshes the available builds by scraping them from an external source.
    /// </summary>
    private async void RefreshAvailableBuilds()
    {
        var results = await this.Mediator.Send(new GetExternalBuildsQuery(BuildPlatform.Windows));

        // TODO: possibly batch this.
        foreach (var externalBuild in results)
        {
            this.CreateBuild(externalBuild);
        }
    }

    /// <summary>
    /// Creates a new local build. Saves us from scraping this info everytime.
    /// </summary>
    /// <param name="externalBuild">The external build.</param>
    private async void CreateBuild(ExternalBuildDto externalBuild)
    {
        await this.Mediator.Send(new CreateBuildCommand(
            Guid.NewGuid(),
            externalBuild.Name,
            externalBuild.Tag,
            externalBuild.Hash,
            externalBuild.DownloadUrl,
            externalBuild.Filesize));
    }
}
