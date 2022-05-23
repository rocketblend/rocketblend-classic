using System.Reactive.Disposables;
using ReactiveUI;
using RocketBlend.Core.ViewModels.Installs;
using RocketBlend.Core.ViewModels.Projects;

namespace RocketBlend.Core.ViewModels;

/// <summary>
/// The main window view model.
/// </summary>
public class MainWindowViewModel : ReactiveObject, IActivatableViewModel, IScreen
{
    private int _selectedTabIndex = 0;

    /// <summary>
    /// Gets the tabs.
    /// </summary>
    private IReadOnlyList<IRoutableViewModel> Tabs { get; }

    ///<inheritdoc/>
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    ///<inheritdoc/>
    public RoutingState Router { get; } = new RoutingState();

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        this.Tabs = new List<IRoutableViewModel>()
        {
            new ProjectsViewModel(this),
            new InstallsViewModel(this),
        };

        this.WhenAnyValue(x => x.SelectedTabIndex).Subscribe(i => this.NavigateToTab(i));

        this.WhenActivated((CompositeDisposable disposables) =>
        {

        });
    }

    /// <summary>
    /// Gets or sets the selected tab index.
    /// </summary>
    public int SelectedTabIndex
    {
        get => this._selectedTabIndex;
        set => this.RaiseAndSetIfChanged(ref this._selectedTabIndex, value);
    }

    /// <summary>
    /// Navigates to the tab.
    /// </summary>
    /// <param name="tabIndex">The tab index.</param>
    private void NavigateToTab(int tabIndex)
    {
        if(tabIndex < this.Tabs.Count)
        {
            this.Router.Navigate.Execute(this.Tabs[tabIndex]);
        }
    }
}
