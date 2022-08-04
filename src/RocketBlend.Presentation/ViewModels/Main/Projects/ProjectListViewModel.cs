using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.Projects;
using RocketBlend.Presentation.Models.SortParameters;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Presentation.ViewModels.Main.Projects;

/// <summary>
/// The project list view model.
/// </summary>
public class ProjectListViewModel : ViewModelBase, IProjectListViewModel, IDisposable
{
    private readonly IDisposable _cleanUp;
    private readonly IProjectService _projectService;
    private readonly IProjectStateService _projectStateService;
    private readonly IProjectFactory _projectFactory;
    private readonly IProjectViewModelFactory _projectViewModelFactory;
    private readonly IDialogService _dialogService;

    private readonly ReadOnlyObservableCollection<IProjectViewModel> _projects;

    private bool _disposedValue;

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    /// <inheritdoc />
    public ProjectSortParameterData SortParameters { get; } = new ProjectSortParameterData();

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IProjectViewModel> Projects => this._projects;

    /// <inheritdoc />
    public ObservableCollectionExtended<IProjectViewModel> SelectedProjects { get; set; }

    /// <inheritdoc />
    [Reactive]
    public IProjectViewModel? SelectedProject { get; private set; }

    /// <inheritdoc />
    [Reactive]
    public bool ShowProjectPane { get; private set; }

    /// <inheritdoc />
    [Reactive]
    public string? SearchText { get; private set; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> CreateProjectCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectListViewModel"/> class.
    /// </summary>
    /// <param name="projectService">The project service.</param>
    /// <param name="projectStateService">The project state service.</param>
    /// <param name="projectFactory">The project factory.</param>
    /// <param name="projectViewModelFactory">The project view model factory.</param>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="screen">The screen.</param>
    public ProjectListViewModel(
        IProjectService projectService,
        IProjectStateService projectStateService,
        IProjectFactory projectFactory,
        IProjectViewModelFactory projectViewModelFactory,
        IDialogService dialogService,
        IScreen screen)
    {
        this._projectService = projectService;
        this._projectStateService = projectStateService;
        this._projectFactory = projectFactory;
        this._projectViewModelFactory = projectViewModelFactory;
        this._dialogService = dialogService;
        this.HostScreen = screen;

        this.SelectedProjects = new();

        this.CreateProjectCommand = ReactiveCommand.CreateFromTask(this.CreateProject);

        // build observable predicate from search text
        var filter = this.WhenValueChanged(t => t.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(250))
            .Select(BuildFilter);

        // build observable sort comparer
        var sort = this.SortParameters.WhenValueChanged(t => t.SelectedItem)
            .WhereNotNull()
            .Select(prop => prop.Comparer)
            .ObserveOn(RxApp.TaskpoolScheduler);

        this.WhenAnyValue(x => x.SelectedProject)
            .Subscribe(x => this.ShowProjectPane = x is not null);

        this.SelectedProjects
            .WhenAnyPropertyChanged()
            .Sample(TimeSpan.FromMilliseconds(150))
            .Subscribe(_ => this.SelectedProject = this.SelectedProjects.LastOrDefault());

        var stream = this._projectStateService.Connect()
            .Filter(filter) // apply user filter
            .IgnoreSameReferenceUpdate()
            .Transform(this.CreateFrom)
            .Sort(sort, SortOptimisations.ComparesImmutableValuesOnly)
            .ObserveOn(RxApp.MainThreadScheduler)
            .DisposeMany()
            .RefCount();

        var viewModels = stream
            .Bind(out this._projects)
            .Subscribe();

        this._cleanUp = new CompositeDisposable(viewModels);
    }

    /// <summary>
    /// Builds the filter.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <returns>A Func.</returns>
    private static Func<ProjectModel, bool> BuildFilter(string? searchText)
    {
        return string.IsNullOrEmpty(searchText) ? (_ => true)
            : (p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Creates the project.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task CreateProject()
    {
        var project = this._projectFactory.Create("New Project", new List<BlendFileModel>());
        await this._projectService.CreateProject(project);
        this.SelectProjectId(project.Id);
    }

    /// <summary>
    /// Selects the project id.
    /// </summary>
    /// <param name="id">The id.</param>
    private void SelectProjectId(Guid id)
    {
        var project = this._projects.FirstOrDefault(x => x.Model.Id == id);
        if(project is not null)
        {
            this.SelectedProjects.Clear();
            this.SelectedProjects.Add(project);
        }
    }

    /// <summary>
    /// Creates the from.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns>An IProjectViewModel.</returns>
    private IProjectViewModel CreateFrom(ProjectModel project) =>
        this._projectViewModelFactory.Create(project);

    #region IDisposable
    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                this._cleanUp.Dispose();
            }

            this._disposedValue = true;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}