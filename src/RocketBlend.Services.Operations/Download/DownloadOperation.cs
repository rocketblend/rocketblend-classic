using System.ComponentModel;
using Downloader;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.Download;

/// <summary>
/// The download operation.
/// </summary>
public class DownloadOperation : StatefulOperationWithProgressBase, IInternalOperation
{
    private readonly IDownload _download;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadOperation"/> class.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="outputDirectory">The output directory.</param>
    public DownloadOperation(Uri url, string outputDirectory)
    {
        this._download = DownloadBuilder.New()
            .WithUrl(url)
            .WithDirectory(outputDirectory)
            .Build();

        this.SubscribeToEvents();
    }

    /// <summary>
    /// Runs the async.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        this.State = OperationState.InProgress;
        await this._download.StartAsync();
    }

    /// <summary>
    /// Subscribes the to events.
    /// </summary>
    private void SubscribeToEvents()
    {
        this._download.ChunkDownloadProgressChanged += this.OnChunkDownloadProgressChanged;
        this._download.DownloadProgressChanged += this.OnDownloadProgressChanged;
        this._download.DownloadFileCompleted += this.OnDownloadFileCompleted;
    }

    /// <summary>
    /// Unsubscribes the from events.
    /// </summary>
    private void UnsubscribeFromEvents()
    {
        this._download.ChunkDownloadProgressChanged -= this.OnChunkDownloadProgressChanged;
        this._download.DownloadProgressChanged -= this.OnDownloadProgressChanged;
        this._download.DownloadFileCompleted -= this.OnDownloadFileCompleted;
    }

    /// <summary>
    /// Ons the chunk download progress changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnChunkDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        //this.CurrentProgress = e.TotalBytesToReceive == 0 ? 0 : ((double)e.ReceivedBytesSize) / e.TotalBytesToReceive;
    }

    /// <summary>
    /// Ons the download progress changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        if (this.State == OperationState.InProgress)
        {
            this.CurrentProgress = e.TotalBytesToReceive == 0 ? 0 : ((double)e.ReceivedBytesSize) / e.TotalBytesToReceive;
        }
    }

    /// <summary>
    /// Ons the download file completed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        this.State = e.Error is null ? OperationState.Finished : OperationState.Failed;
        this.SetFinalProgress();
        //this.UnsubscribeFromEvents();
    }
}