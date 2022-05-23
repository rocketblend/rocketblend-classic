using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels.Projects.Create;

namespace RocketBlend.Views.Projects.Create;

public partial class CreateProjectConfigureView : ReactiveUserControl<CreateProjectConfigureViewModel>
{
    public CreateProjectConfigureView()
    {
        InitializeComponent();

        this.BindInteraction(this.ViewModel, vm => vm.ShowSelectFolderDialog, this.ShowSelectFolderDialog);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task ShowSelectFolderDialog(InteractionContext<Unit, string?> interaction)
    {
        var dialog = new OpenFolderDialog();

        if (Avalonia.Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var folder = await dialog.ShowAsync(desktop.MainWindow);
            interaction.SetOutput(folder);
        }
    }
}
