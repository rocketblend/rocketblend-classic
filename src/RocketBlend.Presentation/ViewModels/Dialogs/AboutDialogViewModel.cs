using RocketBlend.Presentation.Configuration;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Interfaces.Dialogs;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels.Dialogs;
using Splat;

namespace RocketBlend.Presentation.Views.Dialogs;

/// <summary>
/// The about dialog view model.
/// </summary>
public class AboutDialogViewModel : DialogViewModelBase, IAboutDialogViewModel
{
    private readonly AboutDialogConfiguration _aboutDialogConfiguration;

    /// <inheritdoc />
    public string ApplicationVersion { get; }

    /// <inheritdoc />
    public string Credits => string.Join(", ", this._aboutDialogConfiguration.Credits);

    /// <summary>
    /// Initializes a new instance of the <see cref="AboutDialogViewModel"/> class.
    /// </summary>
    public AboutDialogViewModel()
    {
        IApplicationVersionProvider applicationVersionProvider = Locator.Current.GetRequiredService<IApplicationVersionProvider>();

        this._aboutDialogConfiguration = Locator.Current.GetRequiredService<AboutDialogConfiguration>();

        this.ApplicationVersion = applicationVersionProvider.Version;
    }
}