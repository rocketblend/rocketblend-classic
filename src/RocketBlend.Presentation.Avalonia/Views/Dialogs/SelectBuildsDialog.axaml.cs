using Avalonia;
using Avalonia.Markup.Xaml;
using RocketBlend.Presentation.ViewModels.Dialogs.Results;

namespace RocketBlend.Presentation.Avalonia.Views.Dialogs;
/// <summary>
/// The select builds dialog view model.
/// </summary>

public partial class SelectBuildsDialog : DialogWindowBase<SelectBuildsDialogResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SelectBuildsDialog"/> class.
    /// </summary>
    public SelectBuildsDialog()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
