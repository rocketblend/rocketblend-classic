using RocketBlend.FileDownloader.Interfaces;

namespace RocketBlend.FileDownloader;

/// <summary>
/// The file downloader service.
/// </summary>
public class FileDownloaderService : IFileDownloaderService
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileDownloaderService"/> class.
    /// </summary>
    /// <param name="httpClient">The http client.</param>
    public FileDownloaderService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<string> DownloadFile(string url, string downloadPath, CancellationToken cancellationToken = default)
    {
        string extension = Path.GetExtension(url);
        string fileName = $"{Guid.NewGuid()}{extension}";

        downloadPath = Path.Combine(downloadPath, fileName);

        var responseStream = await this._httpClient.GetStreamAsync(url, cancellationToken);
        using var fileStream = new FileStream(downloadPath, FileMode.Create);
        await responseStream.CopyToAsync(fileStream, cancellationToken);
        return fileStream.Name;
    }
}
