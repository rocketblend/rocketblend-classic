using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using RocketBlend.Extensions;
using RocketBlend.Presentation.Services;

namespace RocketBlend.Presentation.ViewModels.Dialogs;

/// <summary>
/// The dialog view model base.
/// </summary>
public class DialogViewModelBase<TResult> : ViewModelBase
    where TResult : DialogResultBase
{
    public event EventHandler<DialogResultEventArgs<TResult>>? CloseRequested;

    /// <summary>
    /// Gets the close command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogViewModelBase"/> class.
    /// </summary>
    protected DialogViewModelBase()
    {
        this.CloseCommand = ReactiveCommand.Create(this.Close);
    }

    /// <summary>
    /// Closes the.
    /// </summary>
    protected void Close() => this.Close(default);

    /// <summary>
    /// Closes the.
    /// </summary>
    /// <param name="result">The result.</param>
    protected void Close(TResult result)
    {
        var args = new DialogResultEventArgs<TResult>(result);

        this.CloseRequested.Raise(this, args);
    }
}

/// <summary>
/// The dialog view model base.
/// </summary>
public class DialogViewModelBase : DialogViewModelBase<DialogResultBase>
{

}
