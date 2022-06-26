namespace RocketBlend.Services.Abstractions.Operations;
/// <summary>
/// The operation with progress.
/// </summary>

public interface IOperationWithProgress
{
    /// <summary>
    /// Gets the current progress.
    /// </summary>
    double CurrentProgress { get; }

    /// <summary>
    /// Gets the progress changed.
    /// </summary>
    public IObservable<double> ProgressChanged { get; }
}