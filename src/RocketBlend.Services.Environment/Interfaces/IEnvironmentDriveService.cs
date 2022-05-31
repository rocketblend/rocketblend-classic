using DriveInfo = RocketBlend.Services.Environment.Models.DriveInfo;

namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The environment drive service.
/// </summary>
public interface IEnvironmentDriveService
{
    /// <summary>
    /// Gets the mounted drives.
    /// </summary>
    /// <returns>A list of DriveInfos.</returns>
    IReadOnlyList<DriveInfo> GetMountedDrives();
}