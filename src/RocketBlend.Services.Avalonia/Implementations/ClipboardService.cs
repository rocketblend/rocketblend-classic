using Avalonia;
using Avalonia.Input;
using Avalonia.Input.Platform;
using RocketBlend.Services.Abstractions.Applications;

namespace RocketBlend.Services.Avalonia.Implementations;

/// <summary>
/// The clipboard service.
/// </summary>
public class ClipboardService : IClipboardService
{
    /// <summary>
    /// Gets the avalonia clipboard.
    /// </summary>
    private static IClipboard? AvaloniaClipboard => Application.Current.Clipboard;

    /// <inheritdoc />
    public Task<string> GetTextAsync() => AvaloniaClipboard.GetTextAsync();

    /// <inheritdoc />
    public async Task<IReadOnlyList<string>> GetFilesAsync()
    {
        var data = await AvaloniaClipboard.GetDataAsync(DataFormats.FileNames);

        return (List<string>)data;
    }

    /// <inheritdoc />
    public Task SetTextAsync(string text) => AvaloniaClipboard.SetTextAsync(text);

    /// <inheritdoc />
    public async Task SetFilesAsync(IReadOnlyList<string> files)
    {
        var dataObject = new DataObject();
        dataObject.Set(DataFormats.FileNames, files);

        await AvaloniaClipboard.SetDataObjectAsync(dataObject);
    }
}
