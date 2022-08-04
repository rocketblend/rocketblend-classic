using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData.Binding;
using ReactiveUI;
using RocketBlend.Presentation.Models.SortParameters;

namespace RocketBlend.Presentation.Interfaces.Main.Projects;

/// <summary>
/// The project list view model interface.
/// </summary>
public interface IProjectListViewModel : IRoutableViewModel
{
    /// <summary>
    /// Gets the projects.
    /// </summary>
    public ReadOnlyObservableCollection<IProjectViewModel> Projects { get; }

    /// <summary>
    /// Gets or sets the selected projects.
    /// </summary>
    public ObservableCollectionExtended<IProjectViewModel> SelectedProjects { get; set; }

    /// <summary>
    /// Gets the sort parameters.
    /// </summary>
    public ProjectSortParameterData SortParameters { get; }

    /// <summary>
    /// Gets the selected project.
    /// </summary>
    public IProjectViewModel? SelectedProject { get; }

    /// <summary>
    /// Gets a value indicating whether show project pane.
    /// </summary>
    public bool ShowProjectPane { get; }

    /// <summary>
    /// Gets the search text.
    /// </summary>
    public string? SearchText { get; }

    /// <summary>
    /// Gets the create project command.
    /// </summary>
    ReactiveCommand<Unit, Unit> CreateProjectCommand { get; }
}