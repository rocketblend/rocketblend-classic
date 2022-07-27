using System.Collections.ObjectModel;
using DynamicData.Binding;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Interfaces.Main.Projects;

namespace RocketBlend.Presentation.Models.SortParameters;

/// <summary>
/// The project sort parameter data.
/// </summary>
public class ProjectSortParameterData : AbstractNotifyPropertyChanged
{
    private readonly IList<SortContainer<IProjectViewModel>> _sortItems = new ObservableCollection<SortContainer<IProjectViewModel>>
    {
        new SortContainer<IProjectViewModel>("Name", SortExpressionComparer<IProjectViewModel>
            .Descending(l => l.Model.Name)),
    };

    /// <summary>
    /// Gets or sets the selected items.
    /// </summary>
    [Reactive]
    public SortContainer<IProjectViewModel> SelectedItem { get; set; }

    /// <summary>
    /// Gets the sort items.
    /// </summary>
    public IEnumerable<SortContainer<IProjectViewModel>> SortItems => this._sortItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectSortParameterData"/> class.
    /// </summary>
    public ProjectSortParameterData()
    {
        this.SelectedItem = this._sortItems[0];
    }
}