using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels;

namespace RocketBlend.Views;

/// <summary>
/// The main window.
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        this.InitializeComponent();
    }
}
