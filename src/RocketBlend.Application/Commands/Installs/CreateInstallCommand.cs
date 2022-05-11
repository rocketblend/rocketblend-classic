using MediatR;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;
using RocketBlend.FileDownloader.Interfaces;

namespace RocketBlend.Application.Commands.Installs;

/// <summary>
/// The create install command.
/// </summary>
public record CreateInstallCommand(Guid Id, Guid BuildId, string LaunchArgs) : ICommand;

/// <summary>
/// The create install command handler.
/// </summary>
public class CreateInstallCommandHandler : ICommandHandler<CreateInstallCommand>
{
    private static readonly string DownloadFolder = ".temp/";
    private static readonly string InstallFolder = "installs/";

    private readonly ICrudService<Install, Guid> _installService;
    private readonly ICrudService<Build, Guid> _buildService;
    private readonly IFileDownloaderService _fileDownloaderService;
    private readonly IFileExtractorService _fileExtractorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInstallCommandHandler"/> class.
    /// </summary>
    /// <param name="installService">The install service.</param>
    /// <param name="buildService">The build service.</param>
    /// <param name="fileDownloaderService">The file downloader service.</param>
    /// <param name="fileExtractorService">The file extractor service.</param>
    public CreateInstallCommandHandler(
        ICrudService<Install, Guid> installService,
        ICrudService<Build, Guid> buildService,
        IFileDownloaderService fileDownloaderService,
        IFileExtractorService fileExtractorService)
    {
        this._installService = installService;
        this._buildService = buildService;
        this._fileDownloaderService = fileDownloaderService;
        this._fileExtractorService = fileExtractorService;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(CreateInstallCommand request, CancellationToken cancellationToken)
    {
        var build = (await this._buildService.GetByIdAsync(request.BuildId, throwIfNotFound: true))!;
        string zipPath = await this._fileDownloaderService.DownloadFile(build.DownloadUrl, GetRelativePath(DownloadFolder), cancellationToken: cancellationToken);

        string extractPath = GetRelativePath(InstallFolder);
        string fileLocation = this._fileExtractorService.ExtractFile(zipPath, extractPath);

        var install = new Install(request.Id, build.Id, "blender.exe", fileLocation, request.LaunchArgs);
        await this._installService.CreateAsync(install, cancellationToken);
        return Unit.Value;
    }

    /// <summary>
    /// Gets the relative path.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <returns>A string.</returns>
    private static string GetRelativePath(string folder)
    {
        string workingPath = Directory.GetCurrentDirectory();
        string downloadPath = Path.Combine(workingPath, folder);
        return Directory.CreateDirectory(downloadPath).FullName; // Ensure path is created.
    }
}
