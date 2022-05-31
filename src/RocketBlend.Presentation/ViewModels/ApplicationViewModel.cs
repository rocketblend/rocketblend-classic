using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Services.Interfaces;

namespace RocketBlend.Presentation.ViewModels;

/// <summary>
/// The application view model.
/// </summary>
public partial class ApplicationViewModel : ViewModelBase, ICanShutdownProvider
{
    private readonly IMainWindowService _mainWindowService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
    /// </summary>
    /// <param name="mainWindowService">The main window service.</param>
    public ApplicationViewModel(IMainWindowService mainWindowService)
    {
        this._mainWindowService = mainWindowService;

        this.QuitCommand = ReactiveCommand.Create(this.ShutDown);

        this.ShowHideCommand = ReactiveCommand.Create(() =>
        {
            if (this.IsMainWindowShown)
            {
                this._mainWindowService.Close();
            }
            else
            {
                this._mainWindowService.Show();
            }
        });

        this.ShowCommand = ReactiveCommand.Create(() => this._mainWindowService.Show());

        //this.AboutCommand = ReactiveCommand.Create(
        //    () => MainViewModel.Instance.DialogScreen.To(new AboutViewModel(navigateBack: MainViewModel.Instance.DialogScreen.CurrentPage is not null)),
        //    canExecute: MainViewModel.Instance.DialogScreen.WhenAnyValue(x => x.CurrentPage).Select(x => x is null));
    }

    /// <summary>
    /// Gets or sets a value indicating whether main window is shown.
    /// </summary>
    [Reactive] public bool IsMainWindowShown { get; set; } = true;

    /// <summary>
    /// Gets the about command.
    /// </summary>
    // public ReactiveCommand<Unit, Unit> AboutCommand { get; }

    /// <summary>
    /// Gets the show command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> ShowCommand { get; }

    /// <summary>
    /// Gets the show hide command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> ShowHideCommand { get; }

    /// <summary>
    /// Gets the quit command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> QuitCommand { get; }

    /// <summary>
    /// Shuts the down.
    /// </summary>
    public void ShutDown() => this._mainWindowService.Shutdown();

    /// <summary>
    /// Ons the shutdown prevented.
    /// </summary>
    public void OnShutdownPrevented()
    {
        //RxApp.MainThreadScheduler.Schedule(() => MainViewModel.Instance.CompactDialogScreen.To(new ShuttingDownViewModel(this)));
    }

    /// <inheritdoc />
    public bool CanShutdown()
    {
        return true;
    }
}
