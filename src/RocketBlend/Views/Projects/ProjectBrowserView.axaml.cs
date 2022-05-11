using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels.Projects;

namespace RocketBlend.Views.Projects;

/// <summary>
/// The project browser view.
/// </summary>
public partial class ProjectBrowserView : ReactiveUserControl<ProjectBrowserViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectBrowserView"/> class.
    /// </summary>
    public ProjectBrowserView()
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
