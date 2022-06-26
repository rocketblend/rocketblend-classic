using System.Reactive.Linq;
using System.Reactive.Subjects;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations;

/// <summary>
/// The operation with progress base.
/// </summary>
public class OperationWithProgressBase : IOperationWithProgress
{
    private readonly ISubject<double> _progressChangedSubject = new Subject<double>();

    private double _progress;

    /// <summary>
    /// Gets or sets the current progress.
    /// </summary>
    public double CurrentProgress
    {
        get => this._progress;
        protected set
        {
            if (Math.Abs(this._progress - value) < 1e-5)
            {
                return;
            }

            this._progress = value;
            this._progressChangedSubject.OnNext(this._progress);
        }
    }

    /// <inheritdoc />
    public IObservable<double> ProgressChanged => this._progressChangedSubject.AsObservable();

    /// <summary>
    /// Sets the final progress.
    /// </summary>
    protected void SetFinalProgress() => this.CurrentProgress = 1;
}