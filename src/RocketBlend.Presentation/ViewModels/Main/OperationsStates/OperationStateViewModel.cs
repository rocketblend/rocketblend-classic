using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.ViewModels.Main.OperationsStates;

/// <summary>
/// The operation state view model.
/// </summary>
public class OperationStateViewModel : ViewModelBase, IOperationStateViewModel
{
    private readonly IPathService _pathService;
    private readonly IOperation _operation;

    /// <inheritdoc />
    public OperationType OperationType => this._operation.Info.OperationType;

    /// <inheritdoc />
    [Reactive]
    public double Progress { get; private set; }

    /// <inheritdoc />
    [Reactive]
    public OperationState State { get; private set; }

    /// <inheritdoc />
    public int SourceFilesCount => this._operation.Info.Files.Count;

    /// <inheritdoc />
    public int SourceDirectoriesCount => this._operation.Info.Directories.Count;

    /// <inheritdoc />
    public bool IsProcessingSingleFile => this.SourceFilesCount + this.SourceDirectoriesCount == 1;

    /// <inheritdoc />
    public string SourceFile => this.IsProcessingSingleFile
        ? this._pathService.GetFileName(this._operation.Info.Directories.FirstOrDefault() ?? this._operation.Info.Files.FirstOrDefault())
        : string.Empty;

    /// <inheritdoc />
    public string SourceDirectory => this._pathService.GetFileName(this._operation.Info.SourceDirectory);

    /// <inheritdoc />
    public string TargetDirectory => this._pathService.GetFileName(this._operation.Info.TargetDirectory);

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationStateViewModel"/> class.
    /// </summary>
    /// <param name="pathService">The path service.</param>
    /// <param name="operation">The operation.</param>
    public OperationStateViewModel(
        IPathService pathService,
        IOperation operation)
    {
        this._pathService = pathService;
        this._operation = operation;

        this.CancelCommand = ReactiveCommand.CreateFromTask(this._operation.CancelAsync);

        var priceRefresher = operation.ProgressChanged
             .Sample(TimeSpan.FromMilliseconds(500))
             .Subscribe(_ => this.Progress = operation.CurrentProgress * 100);

        var stateRefresher = operation.StateChanged
             .Subscribe(_ => this.State = operation.State);
    }
}
