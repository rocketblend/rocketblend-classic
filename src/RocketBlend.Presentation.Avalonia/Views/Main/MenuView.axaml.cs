using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Menu;

namespace RocketBlend.Presentation.Avalonia.Views.Main;

/// <summary>
/// The menu view.
/// </summary>
public partial class MenuView : ReactiveUserControl<IMenuViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuView"/> class.
    /// </summary>
    public MenuView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
