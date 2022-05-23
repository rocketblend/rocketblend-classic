using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels;
using System;
using System.Reactive.Disposables;

namespace RocketBlend.Views;
public partial class CreateProjectWindow : ReactiveWindow<CreateProjectViewModel>
{
    public CreateProjectWindow()
    {
        this.InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.WhenActivated(disposables => {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm => { this.ViewModel.CreateProjectCommand.Subscribe(this.Close); })
                .DisposeWith(disposables);
        });

        //this.WhenActivated(d => d(this.ViewModel!.CreateProjectCommand.Subscribe(this.Close)));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
