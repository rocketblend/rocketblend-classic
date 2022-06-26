using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RocketBlend.Presentation.Avalonia.Views.Main.OperationsStates;
public partial class InactiveOperationsView : UserControl
{
    public InactiveOperationsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
