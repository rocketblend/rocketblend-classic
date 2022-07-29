using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RocketBlend.Presentation.ViewModels.Splash;

namespace RocketBlend.Presentation.Avalonia.Views.Splash;

/// <summary>
/// The splash window.
/// </summary>
public partial class SplashWindow : ReactiveWindow<SplashViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SplashWindow"/> class.
    /// </summary>
    public SplashWindow()
    {
        // The call to WhenActivated is used to execute a block of code
        // when the corresponding view model is activated and deactivated.
        this.InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}