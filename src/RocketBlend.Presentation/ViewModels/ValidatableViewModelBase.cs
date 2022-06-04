using ReactiveUI;
using ReactiveUI.Validation.Helpers;

namespace RocketBlend.Presentation.ViewModels;

/// <summary>
/// The validatable view model base.
/// </summary>
public class ValidatableViewModelBase : ReactiveValidationObject, IActivatableViewModel
{
    /// <inheritdoc />
    public ViewModelActivator Activator { get; } = new ViewModelActivator();
}
