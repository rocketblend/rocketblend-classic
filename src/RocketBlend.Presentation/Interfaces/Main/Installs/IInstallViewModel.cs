using System.Reactive;
using ReactiveUI;

namespace RocketBlend.Presentation.Interfaces.Main.Installs;

/// <summary>
/// The install view model.
/// </summary>
public interface IInstallViewModel
{
    /// <summary>
    /// Gets a value indicating whether is busy.
    /// </summary>
    public bool IsBusy { get; }

    /// <summary>
    /// Gets the id.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the tag.
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Gets the background color.
    /// </summary>
    public string BackgroundColor { get; }

    /// <summary>
    /// Gets the download url.
    /// </summary>
    public string DownloadUrl { get; }

    /// <summary>
    /// Gets the open command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> OpenCommand { get; }

    /// <summary>
    /// Gets the remove command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    /// <summary>
    /// Gets the download command.
    /// </summary>
    public ReactiveCommand<Unit, Unit> DownloadCommand { get; }
}
