using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Archives.Implementations;
using RocketBlend.Services.Archives.Interfaces;
using SharpCompress.Common;
using SharpCompress.Writers;
using ArchiveType = RocketBlend.Services.Abstractions.Models.Enums.ArchiveType;
using InternalArchiveType = SharpCompress.Common.ArchiveType;

namespace RocketBlend.Services.Archives;

/// <summary>
/// The archive processor factory.
/// </summary>
public class ArchiveProcessorFactory : IArchiveProcessorFactory
{
    private readonly IFileService _fileService;
    private readonly IDirectoryService _directoryService;
    private readonly IFileNameGenerationService _fileNameGenerationService;
    private readonly IPathService _pathService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveProcessorFactory"/> class.
    /// </summary>
    /// <param name="fileService">The file service.</param>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="fileNameGenerationService">The file name generation service.</param>
    /// <param name="pathService">The path service.</param>
    public ArchiveProcessorFactory(
        IFileService fileService,
        IDirectoryService directoryService,
        IFileNameGenerationService fileNameGenerationService,
        IPathService pathService)
    {
        this._fileService = fileService;
        this._directoryService = directoryService;
        this._fileNameGenerationService = fileNameGenerationService;
        this._pathService = pathService;
    }

    /// <inheritdoc />
    public IArchiveReader CreateReader(ArchiveType archiveType)
    {
        switch (archiveType)
        {
            case ArchiveType.Zip:
            case ArchiveType.Tar:
            case ArchiveType.TarGz:
            case ArchiveType.TarBz2:
            case ArchiveType.TarXz:
            case ArchiveType.TarLz:
            case ArchiveType.Gz:
            case ArchiveType.SevenZip:
                return this.CreateDefaultArchiveReader();

            case ArchiveType.Xz:
                return this.SingleFileZipArchiveReader(new XzStreamFactory());

            case ArchiveType.Lz:
                return this.SingleFileZipArchiveReader(new LzipStreamFactory());

            case ArchiveType.Bz2:
                return this.SingleFileZipArchiveReader(new Bz2StreamFactory());

            default:
                throw new ArgumentOutOfRangeException(nameof(archiveType), archiveType, null);
        }
    }

    /// <inheritdoc />
    public IArchiveWriter CreateWriter(ArchiveType archiveType) =>
        archiveType switch
        {
            ArchiveType.Tar => this.CreateArchiveWriter(InternalArchiveType.Tar, CompressionType.None),
            ArchiveType.Zip => this.CreateArchiveWriter(InternalArchiveType.Zip, CompressionType.Deflate),
            ArchiveType.TarGz => this.CreateArchiveWriter(InternalArchiveType.Tar, CompressionType.GZip),
            ArchiveType.Gz => this.CreateArchiveWriter(InternalArchiveType.GZip, CompressionType.GZip),
            ArchiveType.TarBz2 => this.CreateArchiveWriter(InternalArchiveType.Tar, CompressionType.BZip2),
            ArchiveType.TarXz => this.CreateArchiveWriter(InternalArchiveType.Tar, CompressionType.Xz),
            ArchiveType.TarLz => this.CreateArchiveWriter(InternalArchiveType.Tar, CompressionType.LZip),
            _ => throw new ArgumentOutOfRangeException(nameof(archiveType), archiveType, null)
        };

    /// <summary>
    /// Creates the default archive reader.
    /// </summary>
    /// <returns>An IArchiveReader.</returns>
    private IArchiveReader CreateDefaultArchiveReader() => new ArchiveReader(this._fileService);

    /// <summary>
    /// Singles the file zip archive reader.
    /// </summary>
    /// <param name="streamFactory">The stream factory.</param>
    /// <returns>An IArchiveReader.</returns>
    private IArchiveReader SingleFileZipArchiveReader(IStreamFactory streamFactory) =>
        new SingleFileZipArchiveReader(this._fileService, this._fileNameGenerationService, this._pathService, streamFactory);

    /// <summary>
    /// Creates the archive writer.
    /// </summary>
    /// <param name="archiveType">The archive type.</param>
    /// <param name="options">The options.</param>
    /// <returns>An IArchiveWriter.</returns>
    private IArchiveWriter CreateArchiveWriter(InternalArchiveType archiveType, WriterOptions options) =>
        new ArchiveWriter(this._fileService, this._pathService, this._directoryService, archiveType, options);
}