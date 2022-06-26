using System.ComponentModel;
using Downloader;

namespace RocketBlend.Services.Downloader.Implementations;

/// <summary>
/// The download.
/// </summary>
public class Download : Interfaces.IDownload
{
    private readonly IDownloadService _downloadService;

    ///<inheritdoc />
    public Guid Id { get; } = Guid.NewGuid();

    ///<inheritdoc />
    public string Url { get; }

    ///<inheritdoc />
    public string Folder { get; }

    ///<inheritdoc />
    public string Filename { get; }

    ///<inheritdoc />
    public long DownloadedFileSize => this._downloadService?.Package?.ReceivedBytesSize ?? 0;

    ///<inheritdoc />
    public long TotalFileSize => this._downloadService?.Package?.TotalFileSize ?? this.DownloadedFileSize;

    ///<inheritdoc />
    public DownloadPackage? Package { get; private set; }

    ///<inheritdoc />
    public DownloadStatus Status { get; private set; }

    ///<inheritdoc />
    public event EventHandler<DownloadProgressChangedEventArgs> ChunkDownloadProgressChanged
    {
        add { this._downloadService.ChunkDownloadProgressChanged += value; }
        remove { this._downloadService.ChunkDownloadProgressChanged -= value; }
    }

    ///<inheritdoc />
    public event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted
    {
        add { this._downloadService.DownloadFileCompleted += value; }
        remove { this._downloadService.DownloadFileCompleted -= value; }
    }

    ///<inheritdoc />
    public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged
    {
        add { this._downloadService.DownloadProgressChanged += value; }
        remove { this._downloadService.DownloadProgressChanged -= value; }
    }

    ///<inheritdoc />
    public event EventHandler<DownloadStartedEventArgs> DownloadStarted
    {
        add { this._downloadService.DownloadStarted += value; }
        remove { this._downloadService.DownloadStarted -= value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Download"/> class.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="path">The path.</param>
    /// <param name="filename">The filename.</param>
    /// <param name="configuration">The configuration.</param>
    public Download(
        string url,
        string path,
        string filename,
        DownloadConfiguration configuration)
    {
        this._downloadService =
            configuration is not null ?
            new DownloadService(configuration) :
            new DownloadService();

        this.Url = url;
        this.Folder = path;
        this.Filename = filename;
        this.Status = DownloadStatus.Created;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Download"/> class.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <param name="configuration">The configuration.</param>
    public Download(
        DownloadPackage package,
        DownloadConfiguration configuration)
    {
        this._downloadService = new DownloadService(configuration);
        this.Package = package;
        this.Status = DownloadStatus.Stopped;
    }

    ///<inheritdoc />>
    public async Task StartAsync()
    {
        if (this.Package is not null)
        {
            await this._downloadService.DownloadFileTaskAsync(this.Package);
            this.Package = this._downloadService.Package;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(this.Filename))
            {
                await this._downloadService.DownloadFileTaskAsync(this.Url, new DirectoryInfo(this.Folder));
            }
            else
            {
                await this._downloadService.DownloadFileTaskAsync(this.Url, Path.Combine(this.Folder, this.Filename));
            }
        }
        this.Status = DownloadStatus.Running;
    }

    ///<inheritdoc />
    public void Stop()
    {
        this._downloadService.CancelAsync();
        this.Status = DownloadStatus.Stopped;
    }

    ///<inheritdoc />
    public void Clear()
    {
        this.Stop();
        this._downloadService.Clear();
        this.Package = null;
        this.Status = DownloadStatus.Created;
    }

    ///<inheritdoc />
    public override bool Equals(object obj)
    {
        return obj is Download download &&
               this.GetHashCode() == download.GetHashCode();
    }

    ///<inheritdoc />
    public override int GetHashCode()
    {
        int hashCode = 37;
        hashCode = hashCode * 7 + this.Url.GetHashCode();
        hashCode = hashCode * 7 + this.DownloadedFileSize.GetHashCode();
        return hashCode;
    }
}