using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RocketBlend.Core.ViewModels.Projects.Create;

/// <summary>
/// The create project configure view model.
/// </summary>
public class CreateProjectConfigureViewModel : ViewModelBase, IRoutableViewModel
{
    /// <inheritdoc />
    public IScreen HostScreen { get; }

    /// <inheritdoc />
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];

    /// <summary>
    /// Gets or sets a value indicating whether open on creation.
    /// </summary>
    [Reactive] public bool OpenOnCreation { get; set; } = false;

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    [Reactive] public string ProjectName { get; set; } = "project1";

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    [Reactive] public string ProjectLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets the open select folder dialog.
    /// </summary>
    public ReactiveCommand<Unit, Unit> OpenSelectFolderDialog { get; }

    /// <summary>
    /// Gets the show select folder dialog.
    /// </summary>
    public Interaction<Unit, string?> ShowSelectFolderDialog { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectConfigureViewModel"/> class.
    /// </summary>
    /// <param name="hostScreen">The host screen.</param>
    public CreateProjectConfigureViewModel(IScreen hostScreen)
    {
        this.HostScreen = hostScreen;

        this.ShowSelectFolderDialog = new Interaction<Unit, string?>();

        this.OpenSelectFolderDialog = ReactiveCommand.CreateFromTask(this.SetProjectLocationFromDialog);
    }

    /// <summary>
    /// Sets the location from dialog.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task SetProjectLocationFromDialog()
    {
        string folderPath = await this.ShowSelectFolderDialog.Handle(Unit.Default);

        if (folderPath is not null)
        {
            this.ProjectLocation = folderPath;
        }
    }
}
