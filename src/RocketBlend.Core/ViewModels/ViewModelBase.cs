using MediatR;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using RocketBlend.Core.Utils;
using Splat;

namespace RocketBlend.Core.ViewModels;

/// <summary>
/// The view model base.
/// </summary>
public class ViewModelBase : ReactiveObject, IActivatableViewModel, IValidatableViewModel
{
    private ISender _mediator = null!;

    /// <summary>
    /// Gets the mediator.
    /// </summary>
    protected ISender Mediator => this._mediator ??= Locator.Current.GetRequiredService<ISender>();

    // protected ObservableAsPropertyHelper<bool> _isBusy;

    /// <summary>
    /// Gets the activator.
    /// </summary>
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    public ValidationContext ValidationContext { get; } = new ValidationContext();

    /// <summary>
    /// Gets a value indicating whether is busy.
    /// </summary>
    // public bool IsBusy { get { return this._isBusy.Value; } }
}
