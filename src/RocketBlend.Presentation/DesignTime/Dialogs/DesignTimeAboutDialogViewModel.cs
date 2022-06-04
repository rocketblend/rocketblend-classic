using RocketBlend.Presentation.Interfaces.Dialogs;
using RocketBlend.Presentation.ViewModels.Dialogs;

namespace RocketBlend.Presentation.DesignTime.Dialogs;

/// <summary>
/// The design time about dialog view model.
/// </summary>
public class DesignTimeAboutDialogViewModel : DialogViewModelBase, IAboutDialogViewModel
{
    /// <inheritdoc />
    public string ApplicationVersion => "1.0.0";

    /// <inheritdoc />
    public string Credits => "User";
}
