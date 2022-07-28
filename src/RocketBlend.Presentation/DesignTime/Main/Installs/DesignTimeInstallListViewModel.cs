using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;

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
    public IInstallViewModel? SelectedInstall { get; }

    /// <inheritdoc />
    public bool ShowInstallPane => true;

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
        this.SelectedInstall = this.Installs.FirstOrDefault();
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