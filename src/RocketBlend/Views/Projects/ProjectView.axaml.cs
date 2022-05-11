using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels.Projects;

namespace RocketBlend.Views.Projects;

public partial class ProjectView : ReactiveUserControl<ProjectViewModel>
{
    public ProjectView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
