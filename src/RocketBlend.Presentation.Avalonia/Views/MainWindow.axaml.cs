using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces;

namespace RocketBlend.Presentation.Avalonia.Views;

/// <summary>
/// The main window.
/// </summary>
public partial class MainWindow : ReactiveWindow<IMainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        // The call to WhenActivated is used to execute a block of code
        // when the corresponding view model is activated and deactivated.
        this.WhenActivated((CompositeDisposable disposable) => { });
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