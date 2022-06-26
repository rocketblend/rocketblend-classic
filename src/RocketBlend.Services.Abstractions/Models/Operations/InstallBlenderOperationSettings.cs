using RocketBlend.Services.Abstractions.Models.Enums;

namespace RocketBlend.Services.Abstractions.Models.Operations;

public record InstallBlenderOperationSettings(Uri SourceUri, string DownloadDirectory, string TargetDirectory)
{
    /// <summary>
    /// Gets the archive type.
    /// </summary>
    public ArchiveType SourceArchiveType
    {
        get
        {
            _ = Enum.TryParse(Path.GetExtension(this.SourceUri.ToString()), true, out ArchiveType archiveType);
            return archiveType;
        }
    }

    /// <summary>
    /// Gets the file name.
    /// </summary>
    public string SourceFileName => Path.GetFileName(this.SourceUri.ToString());

    /// <summary>
    /// Gets the source directory.
    /// </summary>
    public string SourcePath => Path.Combine(this.DownloadDirectory, this.SourceFileName);
}