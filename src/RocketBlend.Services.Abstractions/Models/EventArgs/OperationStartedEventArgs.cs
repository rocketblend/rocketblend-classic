using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Abstractions.Models.EventArgs;

/// <summary>
/// The operation started event args.
/// </summary>
public class OperationStartedEventArgs : System.EventArgs
{
    /// <summary>
    /// Gets the operation.
    /// </summary>
    public IOperation Operation { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationStartedEventArgs"/> class.
    /// </summary>
    /// <param name="operation">The operation.</param>
    public OperationStartedEventArgs(IOperation operation)
    {
        this.Operation = operation;
    }
}