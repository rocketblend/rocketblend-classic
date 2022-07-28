using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Interfaces.Menu;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.Views.Dialogs;
using RocketBlend.Services.Abstractions.Applications;
using Splat;

namespace RocketBlend.Presentation.ViewModels.Menu;

/// <summary>
/// The menu view model.
/// </summary>
public class MenuViewModel : ViewModelBase, IMenuViewModel
{
    private readonly IDialogService _dialogService;

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> ExitCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> AboutCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
    /// </summary>
    public MenuViewModel()
    {
        this._dialogService = Locator.Current.GetRequiredService<IDialogService>();

        IApplicationCloser applicationCloser = Locator.Current.GetRequiredService<IApplicationCloser>();

        this.ExitCommand = ReactiveCommand.Create(applicationCloser.Shutdown);
        this.AboutCommand = ReactiveCommand.CreateFromTask(this.ShowAboutDialogAsync);
    }

    /// <summary>
    /// Shows the about dialog async.
    /// </summary>
    /// <returns>A Task.</returns>
    private Task ShowAboutDialogAsync() => this._dialogService.ShowDialogAsync(nameof(AboutDialogViewModel));
}