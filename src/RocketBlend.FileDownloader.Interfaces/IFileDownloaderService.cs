namespace RocketBlend.FileDownloader.Interfaces;

/// <summary>
/// The downloader service interface.
/// </summary>
public interface IFileDownloaderService
{
    /// <summary>
    /// Downloads the file.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="downloadPath">The download path.</param>
    /// <returns>A Task.</returns>
    public Task<string> DownloadFile(string url, string downloadPath, CancellationToken cancellationToken = default);
}
