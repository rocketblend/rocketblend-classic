using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using RocketBlend.Presentation.Avalonia.State;
using RocketBlend.Presentation.Avalonia.Views;
using RocketBlend.Presentation.ViewModels;
using RocketBlend.Services.Abstractions.Applications;
using Splat;

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
    }

    /// <summary>
    /// The state.
    /// </summary>
    private enum State
    {
        Invalid = 0,
        InitialState,
        Closed,
        Open,
    }

    private readonly StateMachine<State, Trigger> _stateMachine;
    private readonly IClassicDesktopStyleApplicationLifetime _lifetime;
    private CompositeDisposable? _compositeDisposable;
    private bool _hideRequest;
    private bool _isShuttingDown;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationStateManager"/> class.
    /// </summary>
    /// <param name="lifetime">The lifetime.</param>
    /// <param name="startInBg">If true, start in bg.</param>
    internal ApplicationStateManager(IClassicDesktopStyleApplicationLifetime lifetime, bool startInBg)
    {
        this._lifetime = lifetime;
        this._stateMachine = new StateMachine<State, Trigger>(State.InitialState);
        this.ApplicationViewModel = new ApplicationViewModel(this);

        this._stateMachine.Configure(State.InitialState)
            .InitialTransition(State.Open)
            .OnTrigger(Trigger.ShutdownRequested, () => lifetime.Shutdown())
            .OnTrigger(Trigger.ShutdownPrevented, () => this.ApplicationViewModel.OnShutdownPrevented());

        this._stateMachine.Configure(State.Closed)
            .SubstateOf(State.InitialState)
            .OnEntry(() =>
            {
                this._lifetime.MainWindow.Close();
                this._lifetime.MainWindow = null;
                this.ApplicationViewModel.IsMainWindowShown = false;
            })
            .Permit(Trigger.Show, State.Open)
            .Permit(Trigger.ShutdownPrevented, State.Open)
            .Permit(Trigger.Loaded, State.Open);

        this._stateMachine.Configure(State.Open)
            .SubstateOf(State.InitialState)
            .OnEntry(this.CreateAndShowMainWindow)
            .Permit(Trigger.Hide, State.Closed);

        this._lifetime.ShutdownRequested += this.LifetimeOnShutdownRequested;

        this._stateMachine.Start();

        if (!startInBg)
        {
            this._stateMachine.Fire(Trigger.Loaded);
        }
    }

    /// <summary>
    /// Gets the application view model.
    /// </summary>
    internal ApplicationViewModel ApplicationViewModel { get; }

    /// <summary>
    /// Lifetimes the on shutdown requested.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void LifetimeOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        // Shutdown prevention will only work if you directly run the executable.
        e.Cancel = !this.ApplicationViewModel.CanShutdown();

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

        var viewModel = MainWindowViewModel.Instance;

        Locator.CurrentMutable.RegisterConstant<IScreen>(viewModel);

        var result = new MainWindow
        {
            DataContext = MainWindowViewModel.Instance,
        };

        this._compositeDisposable?.Dispose();
        this._compositeDisposable = new();

        // Move to settings.
        var hideOnClose = false;

        Observable.FromEventPattern<CancelEventArgs>(result, nameof(result.Closing))
            .Select(args => (args.EventArgs, !this.ApplicationViewModel.CanShutdown()))
            .TakeWhile(_ => !this._isShuttingDown) // Prevents stack overflow.
            .Subscribe(tup =>
            {
                // _hideRequest flag is used to distinguish what is the user's intent.
                // It is only true when the request comes from the Tray.
                if (hideOnClose || this._hideRequest)
                {
                    this._hideRequest = false; // request processed, set it back to the default.
                    return;
                }

                var (e, preventShutdown) = tup;

                this._isShuttingDown = !preventShutdown;
                e.Cancel = preventShutdown;
                this._stateMachine.Fire(preventShutdown ? Trigger.ShutdownPrevented : Trigger.ShutdownRequested);
            })
            .DisposeWith(this._compositeDisposable);

        Observable.FromEventPattern(result, nameof(result.Closed))
            .Take(1)
            .Subscribe(_ =>
            {
                this._compositeDisposable?.Dispose();
                this._compositeDisposable = null;
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
    void IMainWindowService.Hide()
    {
        this._hideRequest = true;
        this._stateMachine.Fire(Trigger.Hide);
    }

    /// <inheritdoc />
    void IMainWindowService.Shutdown()
    {
        this._stateMachine.Fire(this.ApplicationViewModel.CanShutdown() ? Trigger.ShutdownRequested : Trigger.ShutdownPrevented);
    }
}