using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Presentation.Avalonia.Views;
using RocketBlend.Presentation.Avalonia.Views.Splash;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Infrastructure;
using RocketBlend.Presentation.ViewModels;
using RocketBlend.Presentation.ViewModels.Splash;
using RocketBlend.Services.Abstractions.Applications;
using Splat;

namespace RocketBlend.Presentation.Avalonia;

/// <summary>
/// The app.
/// </summary>
public partial class App : Application
{
    private readonly Func<Task>? _backendInitialiseAsync;
    private ApplicationStateManager? _applicationStateManager;
    private readonly bool _startInBg;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.Name = "RocketBlend";
    }

    public App(Func<Task> backendInitialiseAsync, bool startInBg) : this()
    {
        this._startInBg = startInBg;
        this._backendInitialiseAsync = backendInitialiseAsync;
    }

    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        this.LoadSettings();
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        // Needs to be run after Splat and ReactiveUI are already configured.
        Locator.CurrentMutable.RegisterLazySingleton(() => new ConventionalViewLocator(), typeof(IViewLocator));

        if (!Design.IsDesignMode)
        {
            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var splashViewModel = new SplashViewModel(GetRequiredService<IApplicationVersionProvider>());
                var splash = new SplashWindow { DataContext = splashViewModel };

                splash.Show();

                // Initialize suspension hooks.
                var suspension = new AutoSuspendHelper(this.ApplicationLifetime);
                RxApp.SuspensionHost.CreateNewAppState = () => new MainWindowViewModel();
                RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<MainWindowViewModel>("RocketBlendV2"));
                suspension.OnFrameworkInitializationCompleted();

                Task.Run(async delegate
                {
                    await Task.Delay(2000);
                }).Wait();

                // Read main view model state from disk.
                var state = RxApp.SuspensionHost.GetAppState<MainWindowViewModel>();
                Locator.CurrentMutable.RegisterConstant<IScreen>(state);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = GetRequiredService<IScreen>()
                };

                splash.Close();
            }
        }

        //if (!Design.IsDesignMode)
        //{
        //    if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //    {
        //        this._applicationStateManager = new ApplicationStateManager(desktop, this._startInBg);

        //        this.DataContext = this._applicationStateManager.ApplicationViewModel;

        //        desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        //        RxApp.MainThreadScheduler.Schedule(
        //            async () =>
        //            {
        //                await this._backendInitialiseAsync!(); // Guaranteed not to be null when not in designer.
        //                new MainWindowViewModel().Initialize();
        //            });
        //    }
        //}

        RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Loads the settings.
    /// </summary>
    private void LoadSettings()
    {
        this.LoadTheme();
        LoadLanguage();
    }

    /// <summary>
    /// Loads the theme.
    /// </summary>
    private void LoadTheme()
    {
    }

    /// <summary>
    /// Loads the language.
    /// </summary>
    private static void LoadLanguage()
    {
    }

    /// <summary>
    /// Gets the required service.
    /// </summary>
    /// <returns>A T.</returns>
    private static T GetRequiredService<T>() => Locator.Current.GetRequiredService<T>();
}