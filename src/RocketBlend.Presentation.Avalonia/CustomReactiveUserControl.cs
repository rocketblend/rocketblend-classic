using System;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;

namespace RocketBlend.Presentation.Avalonia;

/// <summary>
/// A ReactiveUI <see cref="UserControl"/> that implements the <see cref="IViewFor{TViewModel}"/> interface and
/// will activate your ViewModel automatically if the view model implements <see cref="IActivatableViewModel"/>.
/// When the DataContext property changes, this class will update the ViewModel property with the new DataContext
/// value, and vice versa.
/// 
/// This class is acting a possible fix for Avalionia incorrectly binding the parent view model to a nulled DataContext.
/// </summary>
/// <typeparam name="TViewModel">ViewModel type.</typeparam>
public class CustomReactiveUserControl<TViewModel> : UserControl, IViewFor<TViewModel> where TViewModel : class
{
    public static readonly StyledProperty<TViewModel?> ViewModelProperty = AvaloniaProperty
        .Register<CustomReactiveUserControl<TViewModel>, TViewModel?>(nameof(ViewModel));

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomReactiveUserControl{TViewModel}"/> class.
    /// </summary>
    public CustomReactiveUserControl()
    {
        // This WhenActivated block calls ViewModel's WhenActivated
        // block if the ViewModel implements IActivatableViewModel.
        this.WhenActivated(disposables => { });
        this.GetObservable(ViewModelProperty).Subscribe(this.OnViewModelChanged);
    }

    /// <summary>
    /// The ViewModel.
    /// </summary>
    public TViewModel? ViewModel
    {
        get => this.GetValue(ViewModelProperty);
        set => this.SetValue(ViewModelProperty, value);
    }

    /// <summary>
    /// Gets or sets the view model.
    /// </summary>
    object? IViewFor.ViewModel
    {
        get => this.ViewModel;
        set => this.ViewModel = (TViewModel?)value;
    }

    /// <summary>
    /// Ons the data context changed.
    /// </summary>
    /// <param name="e">The e.</param>
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        this.ViewModel = this.DataContext as TViewModel;
    }

    /// <summary>
    /// Ons the view model changed.
    /// </summary>
    /// <param name="value">The value.</param>
    private void OnViewModelChanged(object? value)
    {
        if (value == null)
        {
            this.DataContext = null;
            // This breaks stuff with nullable ViewModels.
            // Needs more looking into but I can't figure out why it does what it does.
            ClearValue(DataContextProperty);
        }
        else if (this.DataContext != value)
        {
            this.DataContext = value;
        }
    }
}
