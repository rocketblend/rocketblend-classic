using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Presentation.Interfaces;

namespace RocketBlend.Presentation.ViewModels;

/// <summary>
/// The main window view model.
/// </summary>
[DataContract]
public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
{
    /// <summary>
    /// Gets or sets the router.
    /// </summary>
    [DataMember]
    [Reactive]
    public RoutingState Router { get; set; } = new();

    /// <inheritdoc />
    public string Greeting => "Hello, World!";

    public void Initialize()
    {
    }
}
