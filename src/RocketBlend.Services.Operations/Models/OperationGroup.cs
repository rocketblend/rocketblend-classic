using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.Models;

/// <summary>
/// The operation group.
/// </summary>
public class OperationGroup
{
    /// <summary>
    /// Gets the operations.
    /// </summary>
    public IReadOnlyList<IInternalOperation> Operations { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationGroup"/> class.
    /// </summary>
    /// <param name="operations">The operations.</param>
    public OperationGroup(
        IReadOnlyList<IInternalOperation> operations)
    {
        this.Operations = operations;
    }
}