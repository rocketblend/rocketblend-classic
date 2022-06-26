using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;

namespace RocketBlend.Presentation.Avalonia.Views.Main.Installs;

/// <summary>
/// The install view model.
/// </summary>
public partial class InstallView : ReactiveUserControl<IInstallViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallView"/> class.
    /// </summary>
    public InstallView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
