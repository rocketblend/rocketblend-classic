using System.Reactive.Disposables;
using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Interfaces.Main.Operations;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Presentation.Interfaces.Menu;
using Splat;

namespace RocketBlend.Presentation.ViewModels;

/// <summary>
/// The main window view model.
/// </summary>
[DataContract]
public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
{
    /// <summary>
    /// Gets or sets the router.
    /// </summary>
    [DataMember]
    [Reactive]
    public RoutingState Router { get; set; } = new RoutingState();

    /// <inheritdoc />
    public string Greeting => "Hello, World!";

    /// <inheritdoc />
    public IMenuViewModel MenuViewModel { get; }

    /// <inheritdoc />
    public IOperationsViewModel OperationsViewModel { get; }

    /// <inheritdoc />
    public IOperationsStateViewModel OperationsStateViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        this.MenuViewModel = Locator.Current.GetRequiredService<IMenuViewModel>();
        this.OperationsViewModel = Locator.Current.GetRequiredService<IOperationsViewModel>();
        this.OperationsStateViewModel = Locator.Current.GetRequiredService<IOperationsStateViewModel>();

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            this.Router.Navigate.Execute(Locator.Current.GetRequiredService<IInstallListViewModel>());
        });
    }
}
