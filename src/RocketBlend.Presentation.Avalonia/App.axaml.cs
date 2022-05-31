using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Presentation.Avalonia.Views;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Infrastructure;
using RocketBlend.Presentation.ViewModels;
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

    public App()
    {
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
        //Akavache.Registrations.Start("RocketBlendV1");
        //Akavache.BlobCache.ApplicationName = "RocketBlendV1";

        //Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Initialize suspension hooks.
            var suspension = new AutoSuspendHelper(this.ApplicationLifetime);
            RxApp.SuspensionHost.CreateNewAppState = () => new MainWindowViewModel();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<MainWindowViewModel>("RocketBlendV1"));
            suspension.OnFrameworkInitializationCompleted();

            // Read main view model state from disk.
            var state = RxApp.SuspensionHost.GetAppState<MainWindowViewModel>();
            Locator.CurrentMutable.RegisterConstant<IScreen>(state);

            desktop.MainWindow = new MainWindow
            {
                DataContext = GetRequiredService<IScreen>()
            };
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
