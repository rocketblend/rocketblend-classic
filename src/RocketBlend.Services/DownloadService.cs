using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Akavache;
using DynamicData;
using RocketBlend.Services.Abstractions.Models;
using RocketBlend.Services.Downloader;
using RocketBlend.Services.Downloader.Interfaces;
using BezzadDownloader = Downloader;

namespace RocketBlend.Services;

/// <summary>
/// The download service.
/// </summary>
public class DownloadService : IDisposable, Abstractions.IDownloadService
{
    /// <summary>
    /// The downloads key.
    /// </summary>
    private const string DownloadsKey = "Downloads";

    private readonly IDisposable _cleanUp;
    private readonly IBlobCache _blobCache;

    private readonly SourceCache<DownloadModel, Guid> _downloads = new(x => x.Id);

    private readonly SourceCache<IDownload, Guid> _downloads2 = new(x => x.Id);

    /// <inheritdoc />
    public IObservable<IChangeSet<DownloadModel, Guid>> Connect() => this._downloads.Connect();

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadService"/> class.
    /// </summary>
    public DownloadService()
    {
        this._blobCache = BlobCache.LocalMachine;

        var downloads = this.GetDownloads();
        if (downloads != null)
        {
            this._downloads.Edit(cache =>
            {
                cache.Clear();
                cache.AddOrUpdate(downloads);
            });
        }

        var refreshObjects = this.Connect().SubscribeMany(data =>
        {
            var downloadRefresher = data.DownloadChanged
                .Sample(TimeSpan.FromMilliseconds(500))
                .Subscribe(async _ => await this.AddOrUpdateDownload(data));
                return new CompositeDisposable(downloadRefresher);
        }).DisposeMany().Subscribe();

        //this._cleanUp = this.Connect()
        //    .DisposeMany()
        //    .Subscribe(async _ => await this.Save());
    }

    /// <inheritdoc />
    public async Task AddOrUpdateDownload(DownloadModel download)
    {
        this._downloads.AddOrUpdate(download);
        //await this.Save();
    }

    /// <inheritdoc />
    public async Task RemoveDownload(Guid id)
    {
        this._downloads.RemoveKey(id);
        await this.Save();
    }

    /// <inheritdoc />
    public async Task StartDownload(Guid downloadId)
    {
        var downloadModel = this._downloads.Lookup(downloadId);
        if (!downloadModel.HasValue)
        {
            return;
        }

        if (downloadModel.Value.Package is null)
        {
            await this.StartNewDownload(downloadModel.Value);
        }
        else
        {
            await this.ResumeDownload(downloadModel.Value);
        }
    }

    /// <inheritdoc />
    public async Task StopDownload(Guid downloadId)
    {
        var downloadModel = this._downloads.Lookup(downloadId);
        if (!downloadModel.HasValue || downloadModel.Value.Package is null)
        {
            return;
        }

        var downloader = this.GetDownloaderForPackage(downloadModel.Value.Package);
        downloader.Stop();
    }

    /// <inheritdoc />
    public void Dispose() => this._cleanUp.Dispose();

    /// <summary>
    /// Saves the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Save()
    {
        //await this._blobCache.InsertObject(DownloadsKey, this._downloads.Items);
    }


    /// <summary>
    /// Gets the downloads.
    /// </summary>
    /// <returns>An IEnumerable&lt;DownloadModel&gt;? .</returns>
    private IEnumerable<DownloadModel>? GetDownloads()
    {
        try
        {
            return this._blobCache.GetObject<IEnumerable<DownloadModel>>(DownloadsKey).GetAwaiter().Wait();
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    /// Adds the or update download2.
    /// </summary>
    /// <param name="download">The download.</param>
    /// <returns>A Task.</returns>
    private async Task AddOrUpdateDownload2(IDownload download)
    {
        this._downloads2.AddOrUpdate(download);
    }

    /// <summary>
    /// Starts the new download.
    /// </summary>
    /// <param name="downloadModel">The download model.</param>
    /// <returns>A Task.</returns>
    //private async Task StartNewDownload(DownloadModel downloadModel)
    //{
    //    DirectoryInfo path = new(downloadModel.Folder);

    //    var download = DownloadBuilder.New()
    //        .WithUrl(downloadModel.Url)
    //        .WithDirectory(path.ToString())
    //        .Build();

    //    //download.DownloadProgressChanged += this.OnDownloadProgressChanged!;
    //    //download.DownloadFileCompleted += this.OnDownloadFileCompleted!;
    //    //download.DownloadStarted += this.OnDownloadStarted!;
    //    //download.ChunkDownloadProgressChanged += this.OnChunkDownloadProgressChanged!;

    //    await download.StartAsync();

    //    await this.AddOrUpdateDownload2(download);
    //}

    private async Task StartNewDownload(DownloadModel downloadModel)
    {
        DirectoryInfo path = new(downloadModel.Folder);

        var download = DownloadBuilder.New()
            .WithUrl(downloadModel.Url)
            .WithDirectory(path.ToString())
            .Build();

        download.DownloadProgressChanged += downloadModel.OnDownloadProgressChanged!;
        download.DownloadFileCompleted += downloadModel.OnDownloadFileCompleted!;
        download.DownloadStarted += downloadModel.OnDownloadStarted!;
        download.ChunkDownloadProgressChanged += downloadModel.OnChunkDownloadProgressChanged!;

        //downloadModel.Package = download.Package;
        //await this.AddOrUpdateDownload(downloadModel);

        await download.StartAsync();
    }

    /// <summary>
    /// Resumes the download.
    /// </summary>
    /// <param name="downloadModel">The download model.</param>
    /// <returns>A Task.</returns>
    private async Task ResumeDownload(DownloadModel downloadModel)
    {
        if(downloadModel.Package is null)
        {
            return;
        }

        await this.GetDownloaderForPackage(downloadModel.Package).StartAsync();
    }

    /// <summary>
    /// Gets the downloader for package.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <returns>An IDownload.</returns>
    private IDownload GetDownloaderForPackage(BezzadDownloader.DownloadPackage package)
    {
        return DownloadBuilder.Build(package);
    }
}
