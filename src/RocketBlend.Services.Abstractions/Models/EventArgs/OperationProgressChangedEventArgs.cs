namespace RocketBlend.Services.Abstractions.Models.EventArgs;

/// <summary>
/// The operation progress changed event args.
/// </summary>
public class OperationProgressChangedEventArgs : System.EventArgs
{
    /// <summary>
    /// Gets the current progress.
    /// </summary>
    public double CurrentProgress { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationProgressChangedEventArgs"/> class.
    /// </summary>
    /// <param name="currentProgress">The current progress.</param>
    public OperationProgressChangedEventArgs(double currentProgress)
    {
        this.CurrentProgress = currentProgress;
    }
}