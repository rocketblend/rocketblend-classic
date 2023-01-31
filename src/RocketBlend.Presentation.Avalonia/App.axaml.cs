using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Akavache;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using H.Pipes;
using ReactiveUI;
using RocketBlend.Launcher.Core;
using RocketBlend.Presentation.Avalonia.Views.Splash;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.ViewModels.Splash;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Applications;
using Splat;

namespace RocketBlend.Presentation.Avalonia;

/// <summary>
/// The app.
/// </summary>
public partial class App : Application, IAsyncDisposable
{
    private readonly PipeServer<string[]> _pipeServer = new(Pipes.Default);
    private readonly ISet<string> _clients = new HashSet<string>();

    private readonly bool _startInBackground;
    private readonly Func<Task>? _backendInitialiseAsync;

    private ApplicationStateManager? _applicationStateManager;
    private ILaunchArgumentService? _launchArgumentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.Name = "RocketBlend";
        BlobCache.ApplicationName = this.Name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <param name="backendInitialiseAsync">The backend initialise async.</param>
    public App(Func<Task> backendInitialiseAsync, bool startInBackground) : this()
    {
        this._startInBackground = startInBackground;
        this._backendInitialiseAsync = backendInitialiseAsync;
    }

    /// <inheritdoc />
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        // Needs to be run after Splat and ReactiveUI are already configured.
        Locator.CurrentMutable.RegisterLazySingleton(() => new ConventionalViewLocator(), typeof(IViewLocator));

        if (!Design.IsDesignMode)
        {
            this._launchArgumentService = GetRequiredService<ILaunchArgumentService>();
            this.SetupPipeServer();

            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var splashViewModel = new SplashViewModel(GetRequiredService<IApplicationVersionProvider>());
                var splash = new SplashWindow { DataContext = splashViewModel };

                splash.Show();

                Task.Run(async delegate
                {
                    await Task.Delay(1500);
                }).Wait();

                this._applicationStateManager = new ApplicationStateManager(desktop, this._startInBackground);
                this.DataContext = this._applicationStateManager.ApplicationViewModel;
                desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                RxApp.MainThreadScheduler.Schedule(
                    async () =>
                    {
                        splash.Close();
                        await this.PipeInitializeAsync();
                        this._launchArgumentService?.HandleArguments(Environment.GetCommandLineArgs());
                        await this._backendInitialiseAsync!();
                    });
            }
        }

        RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Setups the pipe server.
    /// </summary>
    private void SetupPipeServer()
    {
        this._pipeServer.ClientConnected += (o, args) =>
        {
            this._clients.Add(args.Connection.PipeName);
            //this.logger.LogDebug($"{args.Connection.PipeName} connected!");

            //try
            //{
            //    string[] response = { "Success", };
            //    await args.Connection.WriteAsync(response).ConfigureAwait(false);
            //}
            //catch (Exception exception)
            //{
            //    this.OnExceptionOccurred(exception);
            //}
        };

        this._pipeServer.MessageReceived += (o, args) =>
        {
            this._launchArgumentService?.HandleArguments(args.Message!);
        };

        this._pipeServer.ClientDisconnected += (o, args) =>
        {
            this._clients.Remove(args.Connection.PipeName);
            //this.logger.LogDebug($"{args.Connection.PipeName} disconnected!");
        };
        
        //this._pipeServer.MessageReceived += (o, args) => this.logger.LogDebug($"{args.Connection.PipeName}: {args.Message}");
        //this._pipeServer.ExceptionOccurred += (o, args) => this.OnExceptionOccurred(args.Exception);
    }

    /// <summary>
    /// Pipes the initialize async.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task PipeInitializeAsync()
    {
        try
        {
            //this._logger.LogDebug("Server starting...");
            await this._pipeServer.StartAsync().ConfigureAwait(false);
            //this._logger.LogDebug("Server is started!");
        }
        catch (Exception exception)
        {
            this.OnExceptionOccurred(exception);
        }
    }

    /// <summary>
    /// Ons the exception occurred.
    /// </summary>
    /// <param name="exception">The exception.</param>
    private void OnExceptionOccurred(Exception exception)
    {
        //this._logger.LogDebug($"Exception: {exception}");
    }

    /// <summary>
    /// Gets the required service.
    /// </summary>
    /// <returns>A T.</returns>
    private static T GetRequiredService<T>() => Locator.Current.GetRequiredService<T>();

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await this._pipeServer.DisposeAsync().ConfigureAwait(false);
    }
}