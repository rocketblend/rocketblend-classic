using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Operations;

namespace RocketBlend.Presentation.Avalonia.Views.Main.Operations;

/// <summary>
/// The operations view.
/// </summary>
public partial class OperationsView : ReactiveUserControl<IOperationsViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OperationsView"/> class.
    /// </summary>
    public OperationsView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
