using RocketBlend.Services.Abstractions.Operations;
using DynamicData;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The operations state service.
/// </summary>
public class OperationsStateService : IOperationsStateService
{
    private readonly SourceList<IOperation> _items;

    /// <inheritdoc />
    public IObservable<IChangeSet<IOperation>> Connect() => this._items.Connect();

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationsStateService"/> class.
    /// </summary>
    public OperationsStateService()
    {
        this._items = new SourceList<IOperation>();
    }

    /// <inheritdoc />
    public void AddOperation(IOperation operation)
    {
        this._items.Add(operation);
    }

    /// <summary>
    /// Removes the operation.
    /// </summary>
    /// <param name="operation">The operation.</param>
    private void RemoveOperation(IOperation operation)
    {
        this._items.Remove(operation);
    }
}