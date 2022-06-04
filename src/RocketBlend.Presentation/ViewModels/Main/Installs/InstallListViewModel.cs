using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs.Results;
using Splat;

namespace RocketBlend.Presentation.ViewModels.Main.Installs;

/// <summary>
/// The install list view model.
/// </summary>
public class InstallListViewModel : ViewModelBase, IInstallListViewModel
{
    private readonly IDialogService _dialogService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallListViewModel"/> class.
    /// </summary>
    /// <param name="screen">The screen.</param>
    public InstallListViewModel(IDialogService dialogService, IScreen? screen = null)
    {
        this._dialogService = dialogService;

        this.HostScreen = screen ?? Locator.Current.GetRequiredService<IScreen>();

        this.SelectBuildsCommand = ReactiveCommand.CreateFromTask(this.ShowSelectBuildsDialogAsync);
    }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> SelectBuildsCommand { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    /// <summary>
    /// Shows the select builds dialog async.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task ShowSelectBuildsDialogAsync()
    {
        var result = await this._dialogService.ShowDialogAsync<SelectBuildsDialogResult>(nameof(SelectBuildsDialogViewModel));
        if(result is not null)
        {
            // Create installs.
        }
    }
}
