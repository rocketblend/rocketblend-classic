using System;
using Avalonia;
using Avalonia.Controls;
using RocketBlend.Presentation.Services;
using RocketBlend.Presentation.ViewModels.Dialogs;

namespace RocketBlend.Presentation.Avalonia.Views.Dialogs;

/// <summary>
/// The dialog window base.
/// </summary>
public class DialogWindowBase<TResult> : Window
    where TResult : DialogResultBase
{
    /// <summary>
    /// Gets the parent window.
    /// </summary>
    private Window ParentWindow => (Window)this.Owner;

    /// <summary>
    /// Gets the view model.
    /// </summary>
    protected DialogViewModelBase<TResult> ViewModel => (DialogViewModelBase<TResult>)this.DataContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogWindowBase"/> class.
    /// </summary>
    protected DialogWindowBase()
    {
        this.SubscribeToViewEvents();
    }

    /// <summary>
    /// Ons the opened.
    /// </summary>
    protected virtual void OnOpened()
    {

    }

    /// <summary>
    /// Ons the opened.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void OnOpened(object sender, EventArgs e)
    {
        this.LockSize();
        this.CenterDialog();

        this.OnOpened();
    }

    /// <summary>
    /// Centers the dialog.
    /// </summary>
    private void CenterDialog()
    {
        var x = this.ParentWindow.Position.X + (this.ParentWindow.Bounds.Width - this.Width) / 2;
        var y = this.ParentWindow.Position.Y + (this.ParentWindow.Bounds.Height - this.Height) / 2;

        this.Position = new PixelPoint((int)x, (int)y);
    }

    /// <summary>
    /// Locks the size.
    /// </summary>
    private void LockSize()
    {
        this.MaxWidth = this.MinWidth = this.Width;
        this.MaxHeight = this.MinHeight = this.Height;
    }

    /// <summary>
    /// Subscribes the to view model events.
    /// </summary>
    private void SubscribeToViewModelEvents() => this.ViewModel.CloseRequested += this.ViewModelOnCloseRequested;

    /// <summary>
    /// Unsubscribes the from view model events.
    /// </summary>
    private void UnsubscribeFromViewModelEvents() => this.ViewModel.CloseRequested -= this.ViewModelOnCloseRequested;

    /// <summary>
    /// Subscribes the to view events.
    /// </summary>
    private void SubscribeToViewEvents()
    {
        DataContextChanged += this.OnDataContextChanged;
        Opened += this.OnOpened;
    }

    /// <summary>
    /// Unsubscribes the from view events.
    /// </summary>
    private void UnsubscribeFromViewEvents()
    {
        DataContextChanged -= this.OnDataContextChanged;
        Opened -= this.OnOpened;
    }

    /// <summary>
    /// Ons the data context changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void OnDataContextChanged(object sender, EventArgs e) => this.SubscribeToViewModelEvents();

    /// <summary>
    /// Views the model on close requested.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The args.</param>
    private void ViewModelOnCloseRequested(object sender, DialogResultEventArgs<TResult> args)
    {
        this.UnsubscribeFromViewModelEvents();
        this.UnsubscribeFromViewEvents();

        this.Close(args.Result);
    }
}

/// <summary>
/// The dialog window base.
/// </summary>
public class DialogWindowBase : DialogWindowBase<DialogResultBase>
{

}