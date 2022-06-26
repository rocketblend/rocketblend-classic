using DynamicData;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The download service.
/// </summary>
public interface IDownloadService
{
    /// <summary>
    /// Connects the.
    /// </summary>
    /// <returns>An IObservable.</returns>
    public IObservable<IChangeSet<DownloadModel, Guid>> Connect();

    /// <summary>
    /// Adds the download.
    /// </summary>
    /// <param name="download">The download.</param>
    public Task AddOrUpdateDownload(DownloadModel download);

    /// <summary>
    /// Starts the download.
    /// </summary>
    /// <param name="id">The id.</param>
    public Task StartDownload(Guid id);

    /// <summary>
    /// Stops the download.
    /// </summary>
    /// <param name="id">The id.</param>
    public Task StopDownload(Guid id);

    /// <summary>
    /// Removes the download.
    /// </summary>
    /// <param name="id">The id.</param>
    public Task RemoveDownload(Guid id);
}
