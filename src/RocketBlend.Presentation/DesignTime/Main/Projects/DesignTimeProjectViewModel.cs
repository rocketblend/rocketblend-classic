using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Projects;
using RocketBlend.Services.Abstractions.Models.Installs;
using RocketBlend.Services.Abstractions.Models.Projects;

namespace RocketBlend.Presentation.DesignTime.Main.Projects;

/// <summary>
/// The design time project view model.
/// </summary>
public class DesignTimeProjectViewModel : IProjectViewModel
{
    /// <inheritdoc/>
    public ProjectModel Model { get; }

    /// <inheritdoc/>
    public ReadOnlyObservableCollection<BlenderInstallModel> Installs { get; }

    /// <inheritdoc/>
    public BlenderInstallModel? SelectedInstall { get; set; }

    /// <inheritdoc/>
    public ReactiveCommand<Unit, Unit> OpenCommand => throw new NotImplementedException();

    /// <inheritdoc/>
    public ReactiveCommand<Unit, Unit> RemoveCommand => throw new NotImplementedException();

    /// <inheritdoc/>
    public ReactiveCommand<Unit, Unit> CreateBlendFileCommand => throw new NotImplementedException();

    /// <inheritdoc/>
    public ReactiveCommand<Unit, Unit> AddBlendFileCommand => throw new NotImplementedException();

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeProjectViewModel"/> class.
    /// </summary>
    public DesignTimeProjectViewModel()
    {
        this.Installs = new(new ObservableCollection<BlenderInstallModel>());
        this.Model = new()
        {
            Id = Guid.NewGuid(),
            Name = "Project name."
        };
    }
}