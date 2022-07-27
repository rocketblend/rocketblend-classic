using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Installs;
using RocketBlend.Services.Abstractions.Models.Projects;

namespace RocketBlend.Presentation.Interfaces.Main.Projects;

/// <summary>
/// The project view model interface.
/// </summary>
public interface IProjectViewModel
{
    /// <summary>
    /// Gets the model.
    /// </summary>
    ProjectModel Model { get; }

    /// <summary>
    /// Gets the installs.
    /// </summary>
    ReadOnlyObservableCollection<BlenderInstallModel> Installs { get; }

    /// <summary>
    /// Gets or sets the selected install.
    /// </summary>
    BlenderInstallModel? SelectedInstall { get; set; }

    /// <summary>
    /// Gets the open command.
    /// </summary>
    ReactiveCommand<Unit, Unit> OpenCommand { get; }

    /// <summary>
    /// Gets the remove command.
    /// </summary>
    ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    /// <summary>
    /// Gets the create blend file command.
    /// </summary>
    ReactiveCommand<Unit, Unit> CreateBlendFileCommand { get; }

    /// <summary>
    /// Gets the add blend file command.
    /// </summary>
    ReactiveCommand<Unit, Unit> AddBlendFileCommand { get; }
}
