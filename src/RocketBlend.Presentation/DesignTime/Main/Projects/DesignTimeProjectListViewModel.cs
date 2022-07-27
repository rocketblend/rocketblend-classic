using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Projects;
using RocketBlend.Presentation.Models.SortParameters;

namespace RocketBlend.Presentation.DesignTime.Main.Projects;

/// <summary>
/// The design time project list view model.
/// </summary>
public class DesignTimeProjectListViewModel : IProjectListViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment => "";

    /// <inheritdoc />
    public IScreen HostScreen => throw new NotImplementedException();

    /// <inheritdoc />
    public ProjectSortParameterData SortParameters { get; } = new ProjectSortParameterData();

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IProjectViewModel> Projects { get; }

    /// <inheritdoc />
    public ObservableCollection<IProjectViewModel> SelectedProjects { get; set; } = new();

    /// <inheritdoc />
    public IProjectViewModel? SelectedProject => this.SelectedProjects.LastOrDefault();

    /// <inheritdoc />
    public bool ShowProjectPane => true;

    /// <inheritdoc />
    public string? SearchText { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> CreateProjectCommand => ReactiveCommand.Create(() => { });

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <inheritdoc />
    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeProjectListViewModel"/> class.
    /// </summary>
    public DesignTimeProjectListViewModel()
    {
        ObservableCollection<IProjectViewModel> list = new()
        {
            new DesignTimeProjectViewModel(),
            new DesignTimeProjectViewModel(),
            new DesignTimeProjectViewModel()
        };
        this.Projects = new(list);

        var _ = list.Remove;

        this.SelectedProjects = new(list);
    }
}
