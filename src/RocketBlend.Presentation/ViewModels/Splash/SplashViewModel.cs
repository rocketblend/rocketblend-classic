using RocketBlend.Services.Abstractions.Applications;

namespace RocketBlend.Presentation.ViewModels.Splash;

/// <summary>
/// The splash view model.
/// </summary>
public class SplashViewModel : ViewModelBase
{
    private readonly IApplicationVersionProvider _applicationVersionProvider;

    /// <summary>
    /// Gets the application version.
    /// </summary>
    public string ApplicationVersion => this._applicationVersionProvider.Version;

    /// <summary>
    /// Initializes a new instance of the <see cref="SplashViewModel"/> class.
    /// </summary>
    /// <param name="applicationVersionProvider">The application version provider.</param>
    public SplashViewModel(IApplicationVersionProvider applicationVersionProvider)
    {
        this._applicationVersionProvider = applicationVersionProvider;
    }
}
