using System.Reactive.Disposables;
using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.Interfaces.Main.Operations;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Presentation.Interfaces.Main.Projects;
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
    /// Gets the tabs.
    /// </summary>
    private readonly List<IRoutableViewModel> _tabViews = new();

    /// <summary>
    /// Gets or sets the router.
    /// </summary>
    [DataMember]
    [Reactive]
    public RoutingState Router { get; private set; } = new RoutingState();

    /// <inheritdoc />
    [Reactive]
    public int SelectedTabIndex { get; private set; }

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
            this._tabViews.Add(Locator.Current.GetRequiredService<IProjectListViewModel>());
            this._tabViews.Add(Locator.Current.GetRequiredService<IInstallListViewModel>());

            this.WhenAnyValue(x => x.SelectedTabIndex).Subscribe(i => this.NavigateToTab(i)).DisposeWith(disposables);
        });
    }

    /// <summary>
    /// Navigates the to tab.
    /// </summary>
    /// <param name="tabIndex">The tab index.</param>
    private void NavigateToTab(int tabIndex)
    {
        if (tabIndex < this._tabViews.Count)
        {
            this.Router.Navigate.Execute(this._tabViews[tabIndex]);
        }
    }
}
