using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Core.ViewModels.Projects.Create;

namespace RocketBlend.Views.Projects.Create;
public partial class CreateProjectSelectBuildView : ReactiveUserControl<CreateProjectSelectBuildViewModel>
{
    public CreateProjectSelectBuildView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
