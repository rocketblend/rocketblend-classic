namespace RocketBlend.Presentation.Interfaces.Dialogs;

/// <summary>
/// The about dialog view model interface.
/// </summary>
public interface IAboutDialogViewModel
{
    /// <summary>
    /// Gets the application version.
    /// </summary>
    string ApplicationVersion { get; }

    /// <summary>
    /// Gets the credits.
    /// </summary>
    string Credits { get; }
}