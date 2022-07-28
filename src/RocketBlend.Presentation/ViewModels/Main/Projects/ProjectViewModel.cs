using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Extensions;
using RocketBlend.Presentation.Interfaces.Main.Projects;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Dialogs;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.Installs;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Presentation.ViewModels.Main.Projects;

/// <summary>
/// The project view model.
/// </summary>
public class ProjectViewModel : ViewModelBase, IProjectViewModel, IDisposable
{
    private readonly IDisposable _cleanUp;
    private readonly ISystemDialogService _systemDialogService;
    private readonly IProjectService _projectService;
    private readonly IBlenderService _blenderService;
    private readonly IBlenderInstallStateService _blenderInstallStateService;

    private readonly ReadOnlyObservableCollection<BlenderInstallModel> _installs;

    private bool _disposedValue;

    /// <inheritdoc />
    [Reactive]
    public ProjectModel Model { get; init; }

    /// <inheritdoc />
    public ReadOnlyObservableCollection<BlenderInstallModel> Installs => this._installs;

    /// <inheritdoc />
    [Reactive]
    public BlenderInstallModel? SelectedInstall { get; set; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> OpenCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> CreateBlendFileCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> AddBlendFileCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectViewModel"/> class.
    /// </summary>
    /// <param name="systemDialogService">The system dialog service.</param>
    /// <param name="projectService">The project service.</param>
    /// <param name="blenderInstallStateService">The blender install state service.</param>
    /// <param name="model">The model.</param>
    public ProjectViewModel(
        ISystemDialogService systemDialogService,
        IProjectService projectService,
        IBlenderService blenderService,
        IBlenderInstallStateService blenderInstallStateService,
        ProjectModel model)
    {
        this._systemDialogService = systemDialogService;
        this._projectService = projectService;
        this._blenderService = blenderService;
        this._blenderInstallStateService = blenderInstallStateService;

        this.Model = model;

        this.OpenCommand = ReactiveCommand.Create(this.Open);
        this.RemoveCommand = ReactiveCommand.Create(this.Remove);
        this.CreateBlendFileCommand = ReactiveCommand.Create(this.CreateBlendFile);
        this.AddBlendFileCommand = ReactiveCommand.Create(this.AddExistingBlendFile);

        var installs = this._blenderInstallStateService.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out this._installs)
            .DisposeMany()
            .Subscribe();

        this.WhenActivated(disposables =>
        {
            // Set initial value
            this.SelectedInstall = this._installs
                .FirstOrDefault(x => x.Id == this.Model.InstallId);

            // Binding
            this.WhenAnyValue(x => x.SelectedInstall)
                .WhereNotNull()
                .Select(x => x.Id)
                .Where(x => x != this.Model.InstallId)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(this.UpdateSelectedInstallId)
                .DisposeWith(disposables);
        });

        this._cleanUp = new CompositeDisposable(installs);
    }

    /// <summary>
    /// Opens the.
    /// </summary>
    private void Open()
    {
        if (this.SelectedInstall is null)
        {
            return;
        }

        var executable = this.SelectedInstall.BlenderExecutable.FullPath;
        this.Model.BlendFiles.ForEach(f => this._blenderService.OpenBlendWith(executable, f.FullPath, this.Model.LaunchArgs));
    }

    /// <summary>
    /// Removes the.
    /// </summary>
    private void Remove()
    {
        this._projectService.DeleteProject(this.Model.Id);
    }

    /// <summary>
    /// Creates the blend file.
    /// </summary>
    private async void CreateBlendFile()
    {
        if (this.SelectedInstall is null)
        {
            return;
        }

        var path = await this._systemDialogService.SaveFileAsync(GetBlendFilter(), "project", ".blend").ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        // TODO: Clean up.
        var executable = this.SelectedInstall.BlenderExecutable.FullPath;
        this._blenderService.CreateBlendFileWith(executable, path);

        this.Model.BlendFiles.Add(CreateBlendFileModel(path));
        await this.Save().ConfigureAwait(false);
    }

    /// <summary>
    /// Adds the existing blend file.
    /// </summary>
    private async void AddExistingBlendFile()
    {
        var path = await this._systemDialogService.GetFileAsync(GetBlendFilter()).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        this.Model.BlendFiles.Add(CreateBlendFileModel(path));
        await this.Save().ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the selected install id.
    /// </summary>
    /// <param name="id">The selected install id.</param>
    private async void UpdateSelectedInstallId(Guid id)
    {
        this.Model.InstallId = id;
        await this.Save().ConfigureAwait(false);
    }

    /// <summary>
    /// Saves the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Save()
    {
        await this._projectService.UpdateProject(this.Model).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the blend filter.
    /// </summary>
    /// <returns>A list of FileDialogFilters.</returns>
    private static List<FileDialogFilter> GetBlendFilter() =>
        new() { new FileDialogFilter() { Name = "Blend Files", Extensions = { "blend" } } };

    /// <summary>
    /// Creates the blend file model.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A BlendFileModel.</returns>
    private static BlendFileModel CreateBlendFileModel(string path) =>
        new()
        {
            Type = FileType.RegularFile,
            Name = Path.GetFileNameWithoutExtension(path),
            FullPath = Path.GetFullPath(path),
        };

    #region IDisposable
    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                this._cleanUp.Dispose();
            }

            this._disposedValue = true;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}