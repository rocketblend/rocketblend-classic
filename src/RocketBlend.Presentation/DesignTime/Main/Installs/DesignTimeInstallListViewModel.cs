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
}
