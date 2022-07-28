using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Installs;

namespace RocketBlend.Presentation.DesignTime.Main.Installs;

/// <summary>
/// The design time install view model.
/// </summary>
public class DesignTimeInstallViewModel : IInstallViewModel
{
    /// <inheritdoc />
    public bool IsBusy => false;

    /// <inheritdoc />
    public Guid Id => Guid.NewGuid();

    /// <inheritdoc />
    public string Name => $"Example:{this.Id}";

    /// <inheritdoc />
    public string Tag => "Release";

    /// <inheritdoc />
    public string BackgroundColor => "#414141";

    /// <inheritdoc />
    public string DownloadUrl => "https://www.example.com/file.zip";

    /// <inheritdoc />
    public double DownloadProgress => 0;

    /// <inheritdoc />
    public double AverageBytesPerSecondSpeed => 0;

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> OpenCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> RemoveCommand => ReactiveCommand.Create(() => { });

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> DownloadCommand => ReactiveCommand.Create(() => { });
}