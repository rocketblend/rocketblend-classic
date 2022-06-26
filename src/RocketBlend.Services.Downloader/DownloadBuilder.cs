using Downloader;

namespace RocketBlend.Services.Downloader;

/// <summary>
/// The download builder.
/// </summary>
public class DownloadBuilder
{
    private string _url = string.Empty;
    private string _directoryPath = string.Empty;
    private string _filename = string.Empty;
    private DownloadConfiguration _downloadConfiguration;

    /// <summary>
    /// News the.
    /// </summary>
    /// <returns>A DownloadBuilder.</returns>
    public static DownloadBuilder New()
    {
        DownloadBuilder builder = new();
        return builder;
    }

    /// <summary>
    /// Builds the.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <returns>An IDownload.</returns>
    public static Interfaces.IDownload Build(DownloadPackage package)
    {
        return Build(package, new DownloadConfiguration());
    }

    /// <summary>
    /// Builds the.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <param name="downloadConfiguration">The download configuration.</param>
    /// <returns>An IDownload.</returns>
    public static Interfaces.IDownload Build(DownloadPackage package, DownloadConfiguration downloadConfiguration)
    {
        return new Implementations.Download(package, downloadConfiguration);
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="DownloadBuilder"/> class from being created.
    /// </summary>
    private DownloadBuilder() { }

    /// <summary>
    /// Withs the url.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithUrl(string url)
    {
        this._url = url;
        return this;
    }

    /// <summary>
    /// Withs the url.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithUrl(Uri url)
    {
        return this.WithUrl(url.AbsoluteUri);
    }

    /// <summary>
    /// Withs the file location.
    /// </summary>
    /// <param name="fullPath">The full path.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithFileLocation(string fullPath)
    {
        fullPath = Path.GetFullPath(fullPath);
        this._filename = Path.GetFileName(fullPath);
        this._directoryPath = Path.GetDirectoryName(fullPath);
        return this;
    }

    /// <summary>
    /// Withs the file location.
    /// </summary>
    /// <param name="uri">The uri.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithFileLocation(Uri uri)
    {
        return this.WithFileLocation(uri.LocalPath);
    }

    /// <summary>
    /// Withs the file location.
    /// </summary>
    /// <param name="fileInfo">The file info.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithFileLocation(FileInfo fileInfo)
    {
        return this.WithFileLocation(fileInfo.FullName);
    }

    /// <summary>
    /// Withs the directory.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithDirectory(string directoryPath)
    {
        this._directoryPath = directoryPath;
        return this;
    }

    /// <summary>
    /// Withs the folder.
    /// </summary>
    /// <param name="folderUri">The folder uri.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithFolder(Uri folderUri)
    {
        return this.WithDirectory(folderUri.LocalPath);
    }

    /// <summary>
    /// Withs the folder.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithFolder(DirectoryInfo folder)
    {
        return this.WithDirectory(folder.FullName);
    }

    /// <summary>
    /// Withs the file name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithFileName(string name)
    {
        this._filename = name;
        return this;
    }

    /// <summary>
    /// Withs the configuration.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder WithConfiguration(DownloadConfiguration configuration)
    {
        this._downloadConfiguration = configuration;
        return this;
    }

    /// <summary>
    /// Configures the.
    /// </summary>
    /// <param name="configure">The configure.</param>
    /// <returns>A DownloadBuilder.</returns>
    public DownloadBuilder Configure(Action<DownloadConfiguration> configure)
    {
        DownloadConfiguration configuration = new();
        configure(configuration);
        return this.WithConfiguration(configuration);
    }

    /// <summary>
    /// Builds the.
    /// </summary>
    /// <returns>An IDownload.</returns>
    public Interfaces.IDownload Build()
    {
        if (string.IsNullOrWhiteSpace(this._url))
        {
            throw new ArgumentNullException($"{nameof(this._url)} has not been declared.");
        }

        return string.IsNullOrWhiteSpace(this._directoryPath)
            ? throw new ArgumentNullException($"{nameof(this._directoryPath)} has not been declared.")
            : (Interfaces.IDownload)new Implementations.Download(this._url, this._directoryPath, this._filename, this._downloadConfiguration);
    }
}