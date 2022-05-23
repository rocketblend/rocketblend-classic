using System.Reactive.Concurrency;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Application.Commands.Builds;
using RocketBlend.Application.Commands.Installs;
using RocketBlend.Application.Queries.Builds.External;
using RocketBlend.Application.Queries.Installs;
using RocketBlend.Core.Models;
using RocketBlend.WebScraper.Blender.Core.Enums;

namespace RocketBlend.Core.ViewModels.Installs;

public class InstallsViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    public ObservableCollectionExtended<InstallModel> Items { get; } = new();

    [Reactive] public InstallModel? SelectedInstall { get; set; }

    public InstallsViewModel(IScreen screen)
    {
        this.HostScreen = screen;

        RxApp.MainThreadScheduler.Schedule(this.LoadInstalls);

        // this.RefreshAvailableBuilds();
        // this.CreateInstall();
    }

    /// <summary>
    /// Loads the installs.
    /// </summary>
    private async void LoadInstalls()
    {
        this.Items.Clear();

        var installs = await this.Mediator.Send(new GetInstallsQuery());

        foreach (var install in installs.Items)
        {
            this.Items.Add(new InstallModel()
            {
                Id = install.Id,
                FileName = install.FileName,
                FileLocation = install.FileLocation,
                Build = new()
                {
                    Id = install.Build.Id,
                    Name = install.Build.Name,
                    Tag = install.Build.Tag,
                    Hash = install.Build.Hash
                }
            });
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
