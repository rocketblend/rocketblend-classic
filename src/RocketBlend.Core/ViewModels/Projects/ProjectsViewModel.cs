using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Application.Commands.Projects;
using RocketBlend.Application.Queries.Installs;
using RocketBlend.Application.Queries.Projects;
using RocketBlend.Core.Models;
using RocketBlend.Core.Utils;
using Splat;

namespace RocketBlend.Core.ViewModels.Projects;

public class ProjectsViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IMapper _mapper;
    private readonly ObservableAsPropertyHelper<bool> _isBusy;

    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    public ObservableCollectionExtended<ProjectModel> Items { get; } = new();
    public ObservableCollectionExtended<InstallModel> AvailableVersions { get; } = new();

    [Reactive] public ProjectModel? SelectedProject { get; set; }
    [Reactive] public string SearchText { get; set; } = string.Empty;
    [Reactive] public bool IsBusy { get; set; }
    [Reactive] public bool IsPaneOpen { get; set; }

    public bool IsBusySaving => _isBusy.Value;

    public ReactiveCommand<Unit, Unit> OpenCreateProjectDialogCommand { get; }
    public ReactiveCommand<Unit, Unit> AddExistingProjectCommand { get; }
    public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveProjectCommand { get; }

    public Interaction<Unit, string?> ShowOpenFileDialog { get; }
    public Interaction<Unit, string?> ShowSelectFolderDialog { get; }
    public Interaction<CreateProjectViewModel, string?> ShowCreateProjectDialog { get; }

    public ProjectsViewModel(IScreen hostScreen)
        :this(hostScreen,
             Locator.Current.GetRequiredService<IMapper>()) { }

    public ProjectsViewModel(IScreen screen, IMapper mapper)
    {
        this.HostScreen = screen;
        this._mapper = mapper;

        this.ShowOpenFileDialog = new Interaction<Unit, string?>();
        this.ShowSelectFolderDialog = new Interaction<Unit, string?>();
        this.ShowCreateProjectDialog = new Interaction<CreateProjectViewModel, string?>();

        this.OpenProjectCommand = ReactiveCommand.Create(this.OpenCurrentProject);
        this.SaveProjectCommand = ReactiveCommand.Create(this.SaveProject);

        this.AddExistingProjectCommand = ReactiveCommand.CreateFromTask(this.GetExistingProject);
        this.OpenCreateProjectDialogCommand = ReactiveCommand.CreateFromTask(this.CreateProject);

        RxApp.MainThreadScheduler.Schedule(this.LoadAvailableVersions);

        this._isBusy = this.SaveProjectCommand
            .IsExecuting
            .ToProperty(this, x => x.IsBusySaving);

        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(this.SearchProjects!);

        this.WhenAny(x => x.SelectedProject.Install, _ => Unit.Default)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .WhereNotNull()
            .InvokeCommand(this.SaveProjectCommand);

        this.WhenAnyValue(x => x.SelectedProject).Subscribe(x => this.IsPaneOpen = this.SelectedProject is not null);
        //this.WhenAnyValue(x => x.SelectedProject).Select(x => x is not null).ToProperty(this, nameof(this.IsPaneOpen));
    }

    private async Task CreateProject()
    {
        var viewModel = new CreateProjectViewModel();
        var result = await this.ShowCreateProjectDialog.Handle(viewModel);

        if (result is not null)
        {
            this.RefreshProjects();
            this.SelectedProject = this.Items.FirstOrDefault(x => x.Id == Guid.Parse(result));
        }
    }

    private async void SearchProjects(string searchString)
    {
        this.IsBusy = true;

        this.Items.Clear();

        var projects = await this.Mediator.Send(new GetProjectsQuery(Name: searchString));
        this.Items.AddRange(this._mapper.Map<List<ProjectModel>>(projects.Items));

        this.IsBusy = false;
    }

    private async void LoadAvailableVersions()
    {
        this.AvailableVersions.Clear();

        var installs = await this.Mediator.Send(new GetInstallsQuery());
        this.AvailableVersions.AddRange(this._mapper.Map<List<InstallModel>>(installs.Items));
    }

    private void OpenCurrentProject()
    {
        if (this.SelectedProject is not null)
        {
            this.OpenSelectedProject(this.SelectedProject);
        }
    }

    private async void OpenSelectedProject(ProjectModel project)
    {
        await this.Mediator.Send(new OpenProjectCommand(project.Id));
    }

    private async Task GetExistingProject()
    {
        string filePath = await this.ShowOpenFileDialog.Handle(Unit.Default);

        if (filePath is not null)
        {
            string fileDirectory = Path.GetDirectoryName(filePath) ?? string.Empty;
            this.AddProject(Path.GetFileName(filePath), fileDirectory);
        }
    }

    private async void AddProject(string fileName, string filePath, Guid? installId = null, string? nameOverride = null)
    {
        Guid id = Guid.NewGuid();
        fileName = fileName.Replace(".blend", string.Empty);

        await this.Mediator.Send(new CreateProjectCommand(id, $"{fileName}.blend", filePath, installId, nameOverride));

        this.RefreshProjects();
        this.SelectedProject = this.Items.FirstOrDefault(x => x.Id == id);
    }

    private async void SaveProject()
    {
        if (this.SelectedProject is null)
        {
            return;
        }

        var project = this.SelectedProject;
        await this.Mediator.Send(new UpdateProjectCommand(project.Id, project.Name, project.FileName, project.FilePath, project.Install.Id));
    }

    private void ClearSearch()
    {
        this.SearchText = string.Empty;
    }

    private async void RefreshProjects()
    {
        this.ClearSearch();
        this.SearchProjects(this.SearchText);
    }
}
