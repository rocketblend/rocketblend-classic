using Avalonia.Markup.Xaml;
using RocketBlend.Presentation.Avalonia.Dialogs;

namespace RocketBlend.Presentation.Avalonia.Views.Dialogs;

/// <summary>
/// The about dialog.
/// </summary>
public partial class AboutDialog : DialogWindowBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AboutDialog"/> class.
    /// </summary>
    public AboutDialog()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}