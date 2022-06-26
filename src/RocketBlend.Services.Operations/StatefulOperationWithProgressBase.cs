using System.Reactive.Linq;
using System.Reactive.Subjects;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The stateful operation with progress base.
/// </summary>
public abstract class StatefulOperationWithProgressBase : OperationWithProgressBase, IStatefulOperation
{
    private readonly ISubject<OperationState> _stateChangedSubject = new Subject<OperationState>();
    private readonly object _stateLocker;

    private OperationState _state;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public OperationState State
    {
        get
        {
            lock (this._stateLocker)
            {
                return this._state;
            }
        }
        protected set
        {
            lock (this._stateLocker)
            {
                if (this._state == value)
                {
                    return;
                }

                this._state = value;
            }

            this._stateChangedSubject.OnNext(value);
        }
    }

    /// <inheritdoc />
    public IObservable<OperationState> StateChanged => this._stateChangedSubject.AsObservable();

    /// <summary>
    /// Initializes a new instance of the <see cref="StatefulOperationWithProgressBase"/> class.
    /// </summary>
    protected StatefulOperationWithProgressBase()
    {
        this._stateLocker = new object();
    }
}