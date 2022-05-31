using System.Threading.Tasks;
using Avalonia.Controls;
using RocketBlend.Avalonia.Interfaces;
using RocketBlend.Presentation.Services.Interfaces;

namespace RocketBlend.Presentation.Avalonia.Services.Implementations;

/// <summary>
/// The system dialog service.
/// </summary>
public class SystemDialogService : ISystemDialogService
{
    private readonly IMainWindowProvider _mainWindowProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemDialogService"/> class.
    /// </summary>
    /// <param name="mainWindowProvider">The main window provider.</param>
    public SystemDialogService(IMainWindowProvider mainWindowProvider)
    {
        this._mainWindowProvider = mainWindowProvider;
    }

    /// <inheritdoc />
    public async Task<string> GetDirectoryAsync(string? initialDirectory = null)
    {
        var dialog = new OpenFolderDialog { Directory = initialDirectory };
        var window = this._mainWindowProvider.GetMainWindow();

        return await dialog.ShowAsync(window);
    }

    /// <inheritdoc />
    public async Task<string> GetFileAsync(string? initialFile = null)
    {
        var dialog = new SaveFileDialog { InitialFileName = initialFile };
        var window = this._mainWindowProvider.GetMainWindow();

        return await dialog.ShowAsync(window);
    }
}
