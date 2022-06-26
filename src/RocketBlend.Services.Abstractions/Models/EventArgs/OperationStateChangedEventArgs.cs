using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models.EventArgs;

/// <summary>
/// The operation state changed event args.
/// </summary>
public class OperationStateChangedEventArgs : System.EventArgs
{
    /// <summary>
    /// Gets the operation state.
    /// </summary>
    public OperationState OperationState { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationStateChangedEventArgs"/> class.
    /// </summary>
    /// <param name="operationState">The operation state.</param>
    public OperationStateChangedEventArgs(OperationState operationState)
    {
        this.OperationState = operationState;
    }
}