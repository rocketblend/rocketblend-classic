using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels.Installs;

namespace RocketBlend.Views;

/// <summary>
/// The install view.
/// </summary>
public partial class InstallView : ReactiveUserControl<InstallViewModel>
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
    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
