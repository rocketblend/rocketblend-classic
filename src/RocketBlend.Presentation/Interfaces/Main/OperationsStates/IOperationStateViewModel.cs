using System.Reactive;
using ReactiveUI;
using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Presentation.Interfaces.Main.OperationsStates;

/// <summary>
/// The operation state view model.
/// </summary>
public interface IOperationStateViewModel : IReactiveNotifyPropertyChanged<IReactiveObject>
{
    /// <summary>
    /// Gets the state.
    /// </summary>
    OperationState State { get; }

    /// <summary>
    /// Gets the operation type.
    /// </summary>
    OperationType OperationType { get; }

    /// <summary>
    /// Gets or sets the progress.
    /// </summary>
    public double Progress { get; }

    /// <summary>
    /// Gets the source files count.
    /// </summary>
    public int SourceFilesCount { get; }

    /// <summary>
    /// Gets the source directories count.
    /// </summary>
    public int SourceDirectoriesCount { get; }

    /// <summary>
    /// Gets a value indicating whether processing single is file.
    /// </summary>
    public bool IsProcessingSingleFile { get; }

    /// <summary>
    /// Gets the source file.
    /// </summary>
    public string SourceFile { get; }

    /// <summary>
    /// Gets the source directory.
    /// </summary>
    public string SourceDirectory { get; }

    /// <summary>
    /// Gets the target directory.
    /// </summary>
    public string TargetDirectory { get; }

    /// <summary>
    /// Gets the cancel command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
}