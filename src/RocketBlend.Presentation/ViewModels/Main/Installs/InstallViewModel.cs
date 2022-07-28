using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Installs;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.ViewModels.Main.Installs;

/// <summary>
/// The install view model.
/// </summary>
public class InstallViewModel : ViewModelBase, IInstallViewModel
{
    private readonly IBlenderInstallStateService _blenderInstallStateService;
    private readonly IOperationsService _operationsService;

    private readonly ObservableAsPropertyHelper<bool> _isBusy;

    private readonly BlenderInstallModel _model;

    /// <inheritdoc />
    public bool IsBusy => this._isBusy.Value;

    /// <inheritdoc />
    public Guid Id => this._model.Id;

    /// <inheritdoc />
    public string Name => this._model.Name;

    /// <inheritdoc />
    public string Tag => this._model.BlenderExecutable.BuildTag;

    /// <inheritdoc />
    public string DownloadUrl => this._model.BlenderExecutable.SourceUri is not null ? this._model.BlenderExecutable.SourceUri.ToString() : "No Download URL!";

    /// <inheritdoc />
    public string BackgroundColor => this._model.BackgroundColor;

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> OpenCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> DownloadCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallViewModel"/> class.
    /// </summary>
    /// <param name="blenderInstallStateService">The blender install service.</param>
    /// <param name="model">The model.</param>
    public InstallViewModel(
        IBlenderInstallStateService blenderInstallStateService,
        IOperationsService operationsService,
        BlenderInstallModel model)
    {
        this._model = model;
        this._blenderInstallStateService = blenderInstallStateService;
        this._operationsService = operationsService;

        this.OpenCommand = ReactiveCommand.Create(this.Open);
        this.RemoveCommand = ReactiveCommand.CreateFromTask(this.Remove);
        this.DownloadCommand = ReactiveCommand.CreateFromTask(this.Download);

        this._isBusy = this.OpenCommand
            .IsExecuting
            .ToProperty(this, x => x.IsBusy);
    }

    /// <summary>
    /// Opens the install.
    /// </summary>
    private void Open() => this._operationsService.OpenFiles(new List<string>() { this._model.BlenderExecutable.FullPath });

    /// <summary>
    /// Removes the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Remove() => await this._blenderInstallStateService.RemoveInstall(this.Id).ConfigureAwait(false);

    /// <summary>
    /// Downloads the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Download()
    {
        var path = Path.GetDirectoryName(this._model.BlenderExecutable.FullPath);
        var parentDirInfo = Directory.GetParent(path ?? string.Empty);
        if (this._model.BlenderExecutable.SourceUri is not null && parentDirInfo is not null)
        {
            var parentDir = parentDirInfo.ToString();
            await this._operationsService.InstallBlenderAsync(
                this._model.BlenderExecutable.SourceUri,
                Path.Combine(parentDir, ".temp"),
                parentDir).ConfigureAwait(false);
        }
    }
}