using DynamicData;

namespace RocketBlend.Services.Abstractions.Operations;

/// <summary>
/// The operations state service.
/// </summary>
public interface IOperationsStateService
{
    /// <summary>
    /// Connects the.
    /// </summary>
    /// <returns>An IObservable.</returns>
    public IObservable<IChangeSet<IOperation>> Connect();

    /// <summary>
    /// Adds the operation.
    /// </summary>
    /// <param name="operation">The operation.</param>
    void AddOperation(IOperation operation);
}