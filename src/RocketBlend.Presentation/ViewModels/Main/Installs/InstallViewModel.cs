using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.ViewModels.Main.Installs;

/// <summary>
/// The install view model.
/// </summary>
public class InstallViewModel : ViewModelBase, IInstallViewModel
{
    private readonly IBlenderInstallService _blenderInstallService;
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
    public string Tag => this._model.Tag;

    /// <inheritdoc />
    public string DownloadUrl => this._model.SourceUri is not null ? this._model.SourceUri.ToString() : "No Download Url!";

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
    /// <param name="blenderInstallService">The blender install service.</param>
    /// <param name="model">The model.</param>
    public InstallViewModel(
        IBlenderInstallService blenderInstallService,
        IOperationsService operationsService,
        BlenderInstallModel model)
    {
        this._model = model;
        this._blenderInstallService = blenderInstallService;
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
    private void Open() => this._operationsService.OpenFiles(new List<string>() { this._model.FullPath });

    /// <summary>
    /// Removes the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Remove() => await this._blenderInstallService.RemoveInstall(this.Id);

    /// <summary>
    /// Downloads the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Download()
    {
        if (this._model.SourceUri is not null)
        {
            await this._operationsService.InstallBlenderAsync(
                this._model.SourceUri,
                Path.Combine(this._model.ParentDirectory, ".temp"),
                this._model.ParentDirectory);
        }
    }
}
