using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;

namespace RocketBlend.Presentation.Avalonia.Views.Main.OperationsStates;

/// <summary>
/// The active operations view.
/// </summary>
public partial class ActiveOperationsView : ReactiveUserControl<IOperationStateViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActiveOperationsView"/> class.
    /// </summary>
    public ActiveOperationsView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
