using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Projects;

namespace RocketBlend.Presentation.Avalonia.Views.Main.Projects;

/// <summary>
/// The project list view.
/// </summary>
public partial class ProjectView : ReactiveUserControl<IProjectViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectView"/> class.
    /// </summary>
    public ProjectView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}