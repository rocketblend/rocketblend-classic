using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels.Installs;

namespace RocketBlend.Views;
public partial class InstallBrowserView : ReactiveUserControl<InstallBrowserViewModel>
{
    public InstallBrowserView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
