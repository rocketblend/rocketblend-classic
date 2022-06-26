using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;
using RocketBlend.Presentation.ViewModels.Main.Installs;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.DesignTime.Main.Installs;

/// <summary>
/// The design time install list view model.
/// </summary>
public class DesignTimeInstallListViewModel : IInstallListViewModel
{
    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> SelectBuildsCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public string? UrlPathSegment => throw new NotImplementedException();

    /// <inheritdoc />
    public IScreen HostScreen => throw new NotImplementedException();

    /// <inheritdoc />
    public ReadOnlyObservableCollection<IInstallViewModel> Installs { get; }

    /// <inheritdoc />
    public BlenderBuildModel? SelectedInstall { get; }

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <inheritdoc />
    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeInstallListViewModel"/> class.
    /// </summary>
    public DesignTimeInstallListViewModel()
    {
        ObservableCollection<IInstallViewModel> list = new()
        {
            GenerateBuild()
        };
        this.Installs = new(list);
    }

    /// <summary>
    /// Generates the build.
    /// </summary>
    /// <returns>A BlenderInstallModel.</returns>
    private static DesignTimeInstallViewModel GenerateBuild()
    {
        return new DesignTimeInstallViewModel();
    }
}
