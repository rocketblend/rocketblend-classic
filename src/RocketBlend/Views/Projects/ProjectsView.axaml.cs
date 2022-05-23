using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RocketBlend.Core.ViewModels;
using RocketBlend.Core.ViewModels.Projects;

namespace RocketBlend.Views.Projects;

public partial class ProjectsView : ReactiveUserControl<ProjectsViewModel>
{
    public ProjectsView()
    {
        this.InitializeComponent();

        this.BindInteraction(this.ViewModel, vm => vm.ShowOpenFileDialog, this.ShowOpenFileDialog);
        this.BindInteraction(this.ViewModel, vm => vm.ShowSelectFolderDialog, this.ShowSelectFolderDialog);
        this.BindInteraction(this.ViewModel, vm => vm.ShowCreateProjectDialog, this.ShowCreateProjectDialog);
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

    private async Task ShowOpenFileDialog(InteractionContext<Unit, string?> interaction)
    {
        var dialog = new OpenFileDialog()
        {
            AllowMultiple = false,
            Filters = new() { new FileDialogFilter() { Name = "Blend Files", Extensions = { "blend" } } },
        };

        if (Avalonia.Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var fileNames = await dialog.ShowAsync(desktop.MainWindow);
            interaction.SetOutput(fileNames?.FirstOrDefault());
        }
    }

    private async Task ShowCreateProjectDialog(InteractionContext<CreateProjectViewModel, string?> interaction)
    {
        var dialog = new CreateProjectWindow();
        dialog.DataContext = interaction.Input;

        if (Avalonia.Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var result = await dialog.ShowDialog<string?>(desktop.MainWindow);
            interaction.SetOutput(result);
        }
    }
}
