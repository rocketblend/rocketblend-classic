using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using RocketBlend.Application.Queries.Installs;
using RocketBlend.Core.Models;
using RocketBlend.Core.Utils;
using Splat;

namespace RocketBlend.Core.ViewModels.Projects.Create;

/// <summary>
/// The create project select build view model.
/// </summary>
public class CreateProjectSelectBuildViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    public ObservableCollectionExtended<InstallModel> RecentInstalls { get; } = new();
    public ObservableCollectionExtended<InstallModel> FilteredInstalls { get; } = new();

    [Reactive] public IReadOnlyList<string> TagFilters { get; set; }
    [Reactive] public InstallModel? SelectedInstall { get; set; }
    [Reactive] public string SearchText { get; set; } = string.Empty;
    [Reactive] public string SelectedTagFilter { get; set; } = string.Empty;
    [Reactive] public bool IsBusy { get; set; }

    public ReactiveCommand<string, Unit> SearchCommand { get; }

    public CreateProjectSelectBuildViewModel(IScreen screen)
        :this(screen,
            Locator.Current.GetRequiredService<IMapper>()){ }

    public CreateProjectSelectBuildViewModel(IScreen screen,  IMapper mapper)
    {
        this.HostScreen = screen;
        this._mapper = mapper;

        this.SearchCommand = ReactiveCommand.Create<string>(this.FilterInstalls);

        RxApp.MainThreadScheduler.Schedule(this.LoadRecentInstalls);

        this.TagFilters = new List<string>()
        {
            "Stable",
            "Release Candidate",
            "Beta",
            "Alpha",
            "Daily",
            "Branch"
        };

        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromSeconds(.25))
            .InvokeCommand(this.SearchCommand);

        this.SetupValidation();
    }

    private async void LoadRecentInstalls()
    {
        this.RecentInstalls.Clear();

        var installs = await this.Mediator.Send(new GetInstallsQuery());
        this.RecentInstalls.AddRange(this._mapper.Map<List<InstallModel>>(installs.Items));
    }

    private async void FilterInstalls(string searchText)
    {
        this.FilteredInstalls.Clear();

        this.IsBusy = true;

        var installs = await this.Mediator.Send(new GetInstallsQuery(SearchQuery: searchText));
        this.FilteredInstalls.AddRange(this._mapper.Map<List<InstallModel>>(installs.Items));

        this.IsBusy = false;
    }

    private void SetupValidation()
    {
        this.ValidationRule(vm => vm.SelectedInstall, install => install != null, "You must specify a valid install.");
    }
}
