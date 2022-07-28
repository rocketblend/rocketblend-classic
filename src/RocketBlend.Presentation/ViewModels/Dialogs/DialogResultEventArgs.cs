namespace RocketBlend.Presentation.ViewModels.Dialogs;

/// <summary>
/// The dialog result event args.
/// </summary>
public class DialogResultEventArgs<TResult> : EventArgs
{
    /// <summary>
    /// Gets the result.
    /// </summary>
    public TResult Result { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogResultEventArgs"/> class.
    /// </summary>
    /// <param name="result">The result.</param>
    public DialogResultEventArgs(TResult result)
    {
        this.Result = result;
    }
}