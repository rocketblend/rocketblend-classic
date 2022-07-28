using DriveType = System.IO.DriveType;

namespace RocketBlend.Services.Environment.Models;

/// <summary>
/// The drive info model.
/// </summary>
public class DriveInfo
{
    /// <summary>
    /// Gets or sets the root directory.
    /// </summary>
    public string RootDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total space bytes.
    /// </summary>
    public long TotalSpaceBytes { get; set; }

    /// <summary>
    /// Gets or sets the free space bytes.
    /// </summary>
    public long FreeSpaceBytes { get; set; }

    /// <summary>
    /// Gets or sets the drive type.
    /// </summary>
    public DriveType DriveType { get; set; }

    /// <summary>
    /// Gets or sets the drive format.
    /// </summary>
    public string DriveFormat { get; set; } = string.Empty;
}