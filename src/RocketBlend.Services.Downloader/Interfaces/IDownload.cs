using PackageDownloader = Downloader;

namespace RocketBlend.Services.Downloader.Interfaces;

/// <summary>
/// The download.
/// </summary>
public interface IDownload: PackageDownloader.IDownload
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the package.
    /// </summary>
    public PackageDownloader.DownloadPackage? Package { get; }
}
