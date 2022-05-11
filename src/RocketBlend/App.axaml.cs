using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using RocketBlend.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using RocketBlend.Views;
using Splat;
using RocketBlend.Core.Utils;
using RocketBlend.Core.ViewModels;

namespace RocketBlend;

/// <summary>
/// The app.
/// </summary>
public partial class App : Avalonia.Application, IApplication
{
    /// <inheritdoc />
    public override void Initialize()
    {
        ApplicationConfigurator.ConfigureServiceMSProvider();
        ApplicationConfigurator.ConfigureServices(Locator.CurrentMutable, this);

        var logger = Locator.Current.GetRequiredService<ILogger<App>>();

        logger.LogInformation("Starting application...");
        ApplicationConfigurator.Configure(Locator.Current);
        logger.LogInformation("Services configured!");

        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        if(this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
            var viewModel = new MainWindowViewModel();

            desktop.MainWindow = new MainWindow()
            {
                DataContext = viewModel 
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <inheritdoc />
    public void Shutdown()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}
