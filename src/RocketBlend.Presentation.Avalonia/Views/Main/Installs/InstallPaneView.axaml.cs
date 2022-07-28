using Avalonia.Markup.Xaml;
using RocketBlend.Presentation.Interfaces.Main.Installs;

namespace RocketBlend.Presentation.Avalonia.Views.Main.Installs;

/// <summary>
/// The install pane view.
/// </summary>
public partial class InstallPaneView : CustomReactiveUserControl<IInstallViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallPaneView"/> class.
    /// </summary>
    public InstallPaneView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
