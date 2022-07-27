using System.Threading.Tasks;
using Avalonia.Controls;
using RocketBlend.Avalonia.Interfaces;
using RocketBlend.Presentation.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
    public async Task<string?> GetDirectoryAsync(string? initialDirectory = null)
    {
        var dialog = new OpenFolderDialog { Directory = initialDirectory };
        var window = this._mainWindowProvider.GetMainWindow();

        return await dialog.ShowAsync(window);
    }

    /// <inheritdoc />
    public async Task<string?> GetFileAsync(IEnumerable<Presentation.Services.Models.FileDialogFilter> filter, string? initialFile = null)
    {
        var files = await this.GetFilesAsync(filter, initialFile, false);
        return files.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string?>> GetFilesAsync(
        IEnumerable<Presentation.Services.Models.FileDialogFilter> filter,
        string? initialFile = null,
        bool allowMultiple = true)
    {
        var dialog = new OpenFileDialog {
            InitialFileName = initialFile,
            AllowMultiple = allowMultiple,
            Filters = filter.Select(f => new FileDialogFilter() { Name = f.Name, Extensions = f.Extensions }).ToList(),
        };
        return await dialog.ShowAsync(this.GetWindow()) ?? Enumerable.Empty<string?>();
    }

    /// <inheritdoc />
    public async Task<string?> SaveFileAsync(
        IEnumerable<Presentation.Services.Models.FileDialogFilter> filter,
            string? initialFile = null,
            string? defaultExtension = null)
    {
        var dialog = new SaveFileDialog()
        {
            InitialFileName = initialFile,
            DefaultExtension = defaultExtension,
            Filters = ConvertFileDialogFilters(filter),
        };

        return await dialog.ShowAsync(this.GetWindow());
    }

    /// <summary>
    /// Gets the window.
    /// </summary>
    /// <returns>A Window.</returns>
    private Window GetWindow() => this._mainWindowProvider.GetMainWindow();

    /// <summary>
    /// Converts the file dialog filters.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns>A list of FileDialogFilters.</returns>
    private static List<FileDialogFilter> ConvertFileDialogFilters(IEnumerable<Presentation.Services.Models.FileDialogFilter> filter) =>
        filter.Select(f => new FileDialogFilter() { Name = f.Name, Extensions = f.Extensions }).ToList();
}
