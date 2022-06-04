using ReactiveUI;

namespace RocketBlend.Presentation.ViewModels;

/// <summary>
/// The view model base.
/// </summary>
public class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    /// <inheritdoc />
    public ViewModelActivator Activator { get; } = new ViewModelActivator();
}
