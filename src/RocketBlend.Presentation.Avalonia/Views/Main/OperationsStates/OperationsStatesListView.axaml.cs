using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.OperationsStates;

namespace RocketBlend.Presentation.Avalonia.Views.Main.OperationsStates;

public partial class OperationsStatesListView : ReactiveUserControl<IOperationsStateViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallListView"/> class.
    /// </summary>
    public OperationsStatesListView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}