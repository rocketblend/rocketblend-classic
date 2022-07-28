using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Presentation.DesignTime.Main.OperationsStates;

/// <summary>
/// The design time operation state view model.
/// </summary>
public class DesignTimeOperationStateViewModel : IOperationStateViewModel
{
    /// <inheritdoc />
    public OperationState State { get; }

    /// <inheritdoc />
    public OperationType OperationType { get; }

    /// <inheritdoc />
    public double Progress { get; }

    /// <inheritdoc />
    public int SourceFilesCount { get; }

    /// <inheritdoc />
    public int SourceDirectoriesCount { get; }

    /// <inheritdoc />
    public bool IsProcessingSingleFile => true;

    /// <inheritdoc />
    public string SourceFile => string.Empty;

    /// <inheritdoc />
    public string SourceDirectory => string.Empty;

    /// <inheritdoc />
    public string TargetDirectory => string.Empty;

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> CancelCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing => throw new NotImplementedException();

    /// <inheritdoc />
    public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed => throw new NotImplementedException();

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeOperationStateViewModel"/> class.
    /// </summary>
    public DesignTimeOperationStateViewModel()
    {
        Random rnd = new();
        this.Progress = rnd.Next(0, 100);
        this.SourceDirectoriesCount = rnd.Next(0, 5);
        this.SourceFilesCount = rnd.Next(0, 5);

        this.OperationType = (OperationType)rnd.Next(Enum.GetNames(typeof(OperationType)).Length);
        this.State = (OperationState)rnd.Next(Enum.GetNames(typeof(OperationState)).Length);
    }

    public IDisposable SuppressChangeNotifications()
    {
        throw new NotImplementedException();
    }
}