using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using RocketBlend.Presentation.Avalonia.Views;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels;
using Stateless;
using System;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using Splat;
using RocketBlend.Presentation.Extensions;
using Microsoft.Extensions.Logging;
using RocketBlend.Services.Abstractions.Applications;

namespace RocketBlend.Presentation.Avalonia;

/// <summary>
/// The application state manager.
/// </summary>
public class ApplicationStateManager : IMainWindowService
{
    /// <summary>
    /// The trigger.
    /// </summary>
    private enum Trigger
    {
        Invalid = 0,
        Hide,
        Show,
        Loaded,
        ShutdownPrevented,
        ShutdownRequested,
        MainWindowClosed,
        BackgroundModeOff,
        BackgroundModeOn,
        Minimised,
        Restored
    }

    /// <summary>
    /// The state.
    /// </summary>
    private enum State
    {
        Invalid = 0,
        Startup,
        BackgroundMode,
        Closed,
        Open,
        StandardMode,
        Hidden,
        Shown
    }

    private readonly StateMachine<State, Trigger> _stateMachine;
    private readonly IClassicDesktopStyleApplicationLifetime _lifetime;
    private CompositeDisposable? _compositeDisposable;

    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationStateManager"/> class.
    /// </summary>
    /// <param name="lifetime">The lifetime.</param>
    /// <param name="startInBg">If true, start in bg.</param>
    internal ApplicationStateManager(IClassicDesktopStyleApplicationLifetime lifetime, bool startInBg)
    {
        startInBg = true;
        this._lifetime = lifetime;
        this._stateMachine = new StateMachine<State, Trigger>(State.Startup);
        this.ApplicationViewModel = new ApplicationViewModel(this);

        this._logger = Locator.Current.GetRequiredService<Microsoft.Extensions.Logging.ILogger>();

        this._stateMachine.Configure(State.BackgroundMode)
            .InitialTransition(State.Open)
            .OnEntryFrom(Trigger.ShutdownRequested, () => lifetime.Shutdown())
            .OnEntryFrom(Trigger.ShutdownPrevented, () => this.ApplicationViewModel.OnShutdownPrevented())
            .Permit(Trigger.BackgroundModeOff, State.StandardMode);

        this._stateMachine.Configure(State.Closed)
            .SubstateOf(State.BackgroundMode)
            .OnEntry(() =>
            {
                this._lifetime.MainWindow = null;
                this.ApplicationViewModel.IsMainWindowShown = false;
            })
            .Permit(Trigger.Show, State.Open)
            .Permit(Trigger.ShutdownPrevented, State.Open)
            .Permit(Trigger.Loaded, State.Open);

        this._stateMachine.Configure(State.Open)
            .SubstateOf(State.BackgroundMode)
            .OnEntry(this.CreateAndShowMainWindow)
            .OnEntryFrom(Trigger.Hide, () => this._lifetime.MainWindow.Close())
            .Permit(Trigger.Hide, State.Closed)
            .Permit(Trigger.MainWindowClosed, State.Closed);

        this._stateMachine.Configure(State.StandardMode)
            .InitialTransition(State.Shown)
            .OnEntryFrom(Trigger.ShutdownPrevented, () => this.ApplicationViewModel.OnShutdownPrevented())
            .OnEntryFrom(Trigger.ShutdownRequested, () => lifetime.Shutdown())
            .Permit(Trigger.BackgroundModeOn, State.BackgroundMode);

        this._stateMachine.Configure(State.Shown)
            .SubstateOf(State.StandardMode)
            .Permit(Trigger.Hide, State.Hidden)
            .Permit(Trigger.Minimised, State.Hidden)
            .OnEntry(() =>
            {
                if (this._lifetime.MainWindow is null)
                {
                    this.CreateAndShowMainWindow();
                }
                else if (this._lifetime.MainWindow.WindowState == WindowState.Minimized)
                {
                    this._lifetime.MainWindow.WindowState = WindowState.Normal;
                }

                this.ApplicationViewModel.IsMainWindowShown = true;
            });

        this._stateMachine.Configure(State.Hidden)
            .SubstateOf(State.StandardMode)
            .Permit(Trigger.Show, State.Shown)
            .Permit(Trigger.Restored, State.Shown)
            .Permit(Trigger.ShutdownPrevented, State.Shown)
            .OnEntry(() =>
            {
                this._lifetime.MainWindow.WindowState = WindowState.Minimized;
                this.ApplicationViewModel.IsMainWindowShown = false;
            });

        this._stateMachine.Configure(State.Startup)
            .Permit(Trigger.BackgroundModeOn, State.BackgroundMode)
            .Permit(Trigger.BackgroundModeOff, State.StandardMode);

        this._stateMachine.OnTransitioned(t => this._logger.LogDebug($"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ", t.Parameters)})"));

        this._lifetime.ShutdownRequested += this.LifetimeOnShutdownRequested;

        this.WhenAnyValue(x => x.HideOnClose)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(backgroundMode => this._stateMachine.Fire(backgroundMode ? Trigger.BackgroundModeOn : Trigger.BackgroundModeOff));

        var test = this._stateMachine.State;

        if (!startInBg)
        {
           this._stateMachine.Fire(Trigger.Loaded);
        }
    }

    /// <summary>
    /// Gets the application view model.
    /// </summary>
    internal ApplicationViewModel ApplicationViewModel { get; }

    [Reactive] public bool HideOnClose { get; set; } = false;

    /// <summary>
    /// Mains the window on closing.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void MainWindowOnClosing(object? sender, CancelEventArgs e)
    {
        if (this._stateMachine.IsInState(State.StandardMode))
        {
            e.Cancel = !this.ApplicationViewModel.CanShutdown();

            if (e.Cancel)
            {
                this._stateMachine.Fire(Trigger.ShutdownPrevented);
            }
            else if (sender is MainWindow w)
            {
                w.Closing -= this.MainWindowOnClosing;
                this._stateMachine.Fire(Trigger.ShutdownRequested);
            }
        }
    }

    /// <summary>
    /// Lifetimes the on shutdown requested.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void LifetimeOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        // Shutdown prevention will only work if you directly run the executable.
        e.Cancel = !this.ApplicationViewModel.CanShutdown();

        this._logger.LogDebug($"Cancellation of the shutdown set to: {e.Cancel}.");

        this._stateMachine.Fire(e.Cancel ? Trigger.ShutdownPrevented : Trigger.ShutdownRequested);
    }

    /// <summary>
    /// Creates the and show main window.
    /// </summary>
    private void CreateAndShowMainWindow()
    {
        if (this._lifetime.MainWindow is { })
        {
            return;
        }

        var result = new MainWindow
        {
            DataContext = new MainWindowViewModel()
        };

        result.Closing += this.MainWindowOnClosing;

        this._compositeDisposable?.Dispose();
        this._compositeDisposable = new();

        result.WhenAnyValue(x => x.WindowState)
            .Skip(1)
            .Subscribe(windowState => this._stateMachine.Fire(windowState == WindowState.Minimized ? Trigger.Minimised : Trigger.Restored))
            .DisposeWith(this._compositeDisposable);

        Observable.FromEventPattern(result, nameof(result.Closed))
            .Take(1)
            .Subscribe(x =>
            {
                this._compositeDisposable?.Dispose();
                this._compositeDisposable = null;
                result.Closing -= this.MainWindowOnClosing;
                this._stateMachine.Fire(Trigger.MainWindowClosed);
            })
            .DisposeWith(this._compositeDisposable);

        this._lifetime.MainWindow = result;

        result.Show();

        this.ApplicationViewModel.IsMainWindowShown = true;
    }

    /// <inheritdoc />
    void IMainWindowService.Show()
    {
        this._stateMachine.Fire(Trigger.Show);
    }

    /// <inheritdoc />
    void IMainWindowService.Close()
    {
        this._stateMachine.Fire(Trigger.Hide);
    }

    /// <inheritdoc />
    void IMainWindowService.Shutdown()
    {
        this._stateMachine.Fire(this.ApplicationViewModel.CanShutdown() ? Trigger.ShutdownRequested : Trigger.ShutdownPrevented);
    }
}
