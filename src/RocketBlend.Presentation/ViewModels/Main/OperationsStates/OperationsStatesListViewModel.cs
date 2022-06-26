using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Aggregation;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Configuration;
using RocketBlend.Presentation.Factories.Interfaces;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;
using RocketBlend.Services.Abstractions.Extensions;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.ViewModels.Main.OperationsStates;

/// <summary>
/// The operations states list view model.
/// </summary>
public class OperationsStatesListViewModel : ViewModelBase, IOperationsStateViewModel
{
    private readonly IOperationStateViewModelFactory _operationStateViewModelFactory;

    private readonly ReadOnlyObservableCollection<IOperationStateViewModel> _activeOperationsViewModels;
    private readonly ReadOnlyObservableCollection<IOperationStateViewModel> _inactiveOperationsViewModels;

    /// <inheritdoc />
    [Reactive]
    public int TotalProgress { get; private set; }

    /// <inheritdoc />
    [Reactive]
    public bool AreAnyOperationsAvailable { get; private set; }

    /// <inheritdoc />
    [Reactive]
    public bool IsLastOperationSuccessful { get; private set; }

    /// <inheritdoc />
    [Reactive]
    public bool IsInProgress { get; private set; }

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IOperationStateViewModel> ActiveOperations => this._activeOperationsViewModels;

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IOperationStateViewModel> InactiveOperations => this._inactiveOperationsViewModels;

    // public IEnumerable<IOperationStateViewModel> InactiveOperations => this._finishedOperationsQueue.Items.Reverse();

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationsStatesListViewModel"/> class.
    /// </summary>
    /// <param name="operationsStateService">The operations state service.</param>
    /// <param name="operationStateViewModelFactory">The operation state view model factory.</param>
    /// <param name="configuration">The configuration.</param>
    public OperationsStatesListViewModel(
        IOperationsStateService operationsStateService,
        IOperationStateViewModelFactory operationStateViewModelFactory,
        OperationsStatesConfiguration configuration)
    {
        this._operationStateViewModelFactory = operationStateViewModelFactory;

        // TODO: Reduct number of connections.
        var activeOperationsViewModels = operationsStateService.Connect()
            .Transform(this.CreateFrom)
            .AutoRefreshOnObservable(x => x.Changed)
            .Filter(x => !x.State.IsCompleted())
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out this._activeOperationsViewModels)
            .DisposeMany()
            .Subscribe();

        var inactiveOperationsViewModels = operationsStateService.Connect()
            .Transform(this.CreateFrom)
            .AutoRefreshOnObservable(x => x.Changed)
            .Filter(x => x.State.IsCompleted())
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out this._inactiveOperationsViewModels)
            .DisposeMany()
            .Subscribe();

        var inProgress = operationsStateService.Connect()
            .AutoRefreshOnObservable(x => x.StateChanged)
            .Subscribe(_ => this.UpdateIsInProgress());

        var available = operationsStateService.Connect()
            .Any()
            .BindTo(this, x => x.AreAnyOperationsAvailable);

        var lastSuccessful = operationsStateService.Connect()
            .AutoRefreshOnObservable(x => x.StateChanged)
            .Filter(x => x.State == OperationState.Finished)
            .Any()
            .BindTo(this, x => x.IsLastOperationSuccessful);

        var totalProgress = operationsStateService.Connect()
            .AutoRefreshOnObservable(x => x.ProgressChanged)
            .Sample(TimeSpan.FromMilliseconds(500))
            .Subscribe(_ => this.UpdateProgress());
    }

    /// <summary>
    /// Updates the is in progress.
    /// </summary>
    private void UpdateIsInProgress()
    {
        this.IsInProgress = this.ActiveOperations.Any();
    }

    /// <summary>
    /// Updates the progress.
    /// </summary>
    private void UpdateProgress()
    {
        if (!this.ActiveOperations.Any())
        {
            this.TotalProgress = default;
            return;
        }
        var averageProgress = this.ActiveOperations.Average(o => o.Progress);
        this.TotalProgress = (int)(averageProgress);
    }

    /// <summary>
    /// Creates the from.
    /// </summary>
    /// <param name="operation">The operation.</param>
    /// <returns>An IOperationStateViewModel.</returns>
    private IOperationStateViewModel CreateFrom(IOperation operation) =>
        this._operationStateViewModelFactory.Create(operation);
}
