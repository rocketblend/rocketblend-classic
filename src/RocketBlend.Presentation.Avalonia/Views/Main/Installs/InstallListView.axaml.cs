using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;

namespace RocketBlend.Presentation.Avalonia.Views.Main.Installs;

/// <summary>
/// The install list view.
/// </summary>
public partial class InstallListView : ReactiveUserControl<IInstallListViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallListView"/> class.
    /// </summary>
    public InstallListView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
