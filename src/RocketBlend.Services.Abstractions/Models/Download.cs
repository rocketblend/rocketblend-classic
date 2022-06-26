using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Downloader;

namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The download model.
/// </summary>
public class DownloadModel : IHasKey<Guid>
{
    private readonly ISubject<DownloadModel> _downloadChangedSubject = new Subject<DownloadModel>();

    /// <summary>
    /// Gets the download progress changed.
    /// </summary>
    public IObservable<DownloadModel> DownloadChanged => this._downloadChangedSubject.AsObservable();

    /// <inheritdoc />
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Gets the url.
    /// </summary>
    public string Url { get; } = string.Empty;

    /// <summary>
    /// Gets the folder.
    /// </summary>
    public string Folder { get; } = string.Empty;

    /// <summary>
    /// Gets the filename.
    /// </summary>
    public string Filename { get; private set; } = string.Empty;

    /// <summary>
    ///     Gets the number of received bytes.
    /// </summary>
    /// <returns>An System.Int64 value that indicates the number of received bytes.</returns>
    public long ReceivedBytesSize { get; private set; }

    /// <summary>
    ///     Gets the total number of bytes in a System.Net.WebClient data download operation.
    /// </summary>
    /// <returns>An System.Int64 value that indicates the number of bytes that will be received.</returns>
    public long TotalBytesToReceive { get; private set; }

    /// <summary>
    ///     How many bytes downloaded per second
    /// </summary>
    public double BytesPerSecondSpeed { get; private set; }

    /// <summary>
    ///     Average download speed
    /// </summary>
    public double AverageBytesPerSecondSpeed { get; set; }

    /// <summary>
    /// Gets the package.
    /// </summary>
    public DownloadPackage? Package { get; set; }


    /// <summary>
    /// Gets or sets the progress percentage.
    /// </summary>
    public double ProgressPercentage { get; set; } = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadModel"/> class.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="folder">The folder.</param>
    /// <param name="filename">The filename.</param>
    public DownloadModel(string url, string folder, string? filename = "")
    {
        this.Url = url;
        this.Folder = folder;
        this.Filename = filename ?? string.Empty;
    }

    /// <summary>
    /// Ons the download started.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnDownloadStarted(object sender, DownloadStartedEventArgs e)
    {
        this.Filename = e.FileName;
        this.TotalBytesToReceive = e.TotalBytesToReceive;
        this._downloadChangedSubject.OnNext(this);
    }

    /// <summary>
    /// On the download file completed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
    }

    /// <summary>
    /// Ons the chunk download progress changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnChunkDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        this.ProgressPercentage = e.ProgressPercentage;
        this._downloadChangedSubject.OnNext(this);
    }

    /// <summary>
    /// Ons the download progress changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        this.AverageBytesPerSecondSpeed = e.AverageBytesPerSecondSpeed;
        this.BytesPerSecondSpeed = e.BytesPerSecondSpeed;
    }
}
