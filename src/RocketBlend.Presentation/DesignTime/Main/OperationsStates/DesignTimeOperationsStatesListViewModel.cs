using System.Collections.ObjectModel;
using System.ComponentModel;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;

namespace RocketBlend.Presentation.DesignTime.Main.OperationsStates;

/// <summary>
/// The design time operations states list view model.
/// </summary>
public class DesignTimeOperationsStatesListViewModel : IOperationsStateViewModel
{
    /// <inheritdoc />
    public int TotalProgress => 80;

    /// <inheritdoc />
    public bool AreAnyOperationsAvailable => true;

    /// <inheritdoc />
    public bool IsLastOperationSuccessful => true;

    /// <inheritdoc />
    public bool IsInProgress => true;

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IOperationStateViewModel> ActiveOperations { get; }

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IOperationStateViewModel> InactiveOperations { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeOperationsStatesListViewModel"/> class.
    /// </summary>
    public DesignTimeOperationsStatesListViewModel()
    {
        ObservableCollection<IOperationStateViewModel> list = new()
        {
            new DesignTimeOperationStateViewModel(),
            new DesignTimeOperationStateViewModel(),
            new DesignTimeOperationStateViewModel(),
        };

        this.ActiveOperations = new(list);
        this.InactiveOperations = new(new ObservableCollection<IOperationStateViewModel>());
    }
}
