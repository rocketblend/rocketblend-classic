using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Core.ViewModels.Installs;

namespace RocketBlend.Views.Installs;
public partial class InstallsView : ReactiveUserControl<InstallsViewModel>
{
    public InstallsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
