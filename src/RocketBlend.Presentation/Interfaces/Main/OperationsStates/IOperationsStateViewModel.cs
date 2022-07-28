using System.Collections.ObjectModel;

namespace RocketBlend.Presentation.Interfaces.Main.OperationsStates;

/// <summary>
/// The operations state view model.
/// </summary>
public interface IOperationsStateViewModel
{
    /// <summary>
    /// Gets or sets the total progress.
    /// </summary>
    public int TotalProgress { get; }

    /// <summary>
    /// Gets a value indicating whether are any operations available.
    /// </summary>
    public bool AreAnyOperationsAvailable { get; }

    /// <summary>
    /// Gets a value indicating whether last operation is successful.
    /// </summary>
    public bool IsLastOperationSuccessful { get; }

    /// <summary>
    /// Gets a value indicating whether in is progress.
    /// </summary>
    public bool IsInProgress { get; }

    /// <summary>
    /// Gets the active operations.
    /// </summary>
    public ReadOnlyObservableCollection<IOperationStateViewModel> ActiveOperations { get; }

    /// <summary>
    /// Gets the inactive operations.
    /// </summary>
    public ReadOnlyObservableCollection<IOperationStateViewModel> InactiveOperations { get; }
}