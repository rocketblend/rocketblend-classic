using ReactiveUI.Fody.Helpers;

namespace RocketBlend.Core.ViewModels;

/// <summary>
/// The view model base.
/// </summary>
public class ViewModelBase<TModel> : ViewModelBase
{
    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    [Reactive] public TModel Model { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public ViewModelBase(TModel model)
    {
        this.Model = model;
    }
}
