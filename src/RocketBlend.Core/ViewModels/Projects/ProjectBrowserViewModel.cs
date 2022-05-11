using System.Reactive.Linq;
using DynamicData.Binding;
using ReactiveUI;
using RocketBlend.Application.Queries.Projects;
using RocketBlend.Blender.Interfaces;
using RocketBlend.Core.Utils;
using Splat;

namespace RocketBlend.Core.ViewModels.Projects;

/// <summary>
/// The main window view model.
/// </summary>
public class ProjectBrowserViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IBlenderClient _blenderClient;

    private bool _isBusy;
    private bool _isSideBarOpen;
    private string? _searchText;

    private ProjectViewModel? _selectedProject;

    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectBrowserViewModel"/> class.
    /// </summary>
    /// <param name="screen">The screen.</param>
    public ProjectBrowserViewModel(IScreen screen)
        : this(screen, Locator.Current.GetRequiredService<IBlenderClient>()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectBrowserViewModel"/> class.
    /// </summary>
    /// <param name="screen">The screen.</param>
    /// <param name="blenderClient">The blender client.</param>
    public ProjectBrowserViewModel(IScreen screen, IBlenderClient blenderClient)
    {
        this.HostScreen = screen;
        this._blenderClient = blenderClient;

        // this._selectedProject = new ProjectViewModel(new ProjectDto() { Name = "Select a project" });

        this.BindSearch();
    }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    /// <summary>
    /// Gets or sets the selected project.
    /// </summary>
    public ProjectViewModel? SelectedProject
    {
        get => this._selectedProject;
        set
        {
            this.RaiseAndSetIfChanged(ref this._selectedProject, value);
            this.IsSideBarOpen = this._selectedProject is not null;
        }
    }

    /// <summary>
    /// Gets or sets the search text.
    /// </summary>
    public string? SearchText
    {
        get => this._searchText;
        set => this.RaiseAndSetIfChanged(ref this._searchText, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether is busy.
    /// </summary>
    public bool IsBusy
    {
        get => this._isBusy;
        set => this.RaiseAndSetIfChanged(ref this._isBusy, value);
    }

    /// <summary>
    /// Gets a value indicating whether has selection.
    /// </summary>
    public bool IsSideBarOpen
    {
        get => this._isSideBarOpen;
        set => this.RaiseAndSetIfChanged(ref this._isSideBarOpen, value);
    }

    /// <summary>
    /// Gets the search results.
    /// </summary>
    public ObservableCollectionExtended<ProjectViewModel> SearchResults { get; } = new();

    /// <inheritdoc />
    public void OpenSelectedProject()
    {
        if(this._selectedProject is not null)
        {
            this.OpenProject(this._selectedProject);
        }
    }

    //private async void Create()
    //{
    //    var project = new Project(Guid.NewGuid(), "Project B", "D:\\Creative\\Blender\\Projects\\Testing\\Project B\\Project-B.blend");
    //    await this._crudService.CreateAsync(project);
    //}

    /// <summary>
    /// Searches the.
    /// </summary>
    /// <param name="searchString">The search string.</param>
    private async void Search(string searchString)
    {
        this.IsBusy = true;
        this.SearchResults.Clear();

        this._cancellationTokenSource?.Cancel();
        this._cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = this._cancellationTokenSource.Token;

        var projects = await this.Mediator.Send(new GetProjectsQuery(Name: searchString), cancellationToken);

        foreach (var project in projects.Items)
        {
            this.SearchResults.Add(new ProjectViewModel(project));
        }

        this.IsBusy = false;
    }

    /// <summary>
    /// Opens the project.
    /// </summary>
    /// <param name="project">The project.</param>
    private void OpenProject(ProjectViewModel project)
    {
        this._blenderClient.Executable = project.InstallExecutable;

        this._blenderClient.OpenProject(project.Path);
    }

    /// <summary>
    /// Binds the search.
    /// </summary>
    /// <returns>An IDisposable.</returns>
    private IDisposable BindSearch()
    {
        return this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(this.Search!);
    }
}