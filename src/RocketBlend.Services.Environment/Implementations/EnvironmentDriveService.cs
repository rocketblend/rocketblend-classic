using RocketBlend.Extensions;
using RocketBlend.Services.Environment.Interfaces;
using IoDriveInfo = System.IO.DriveInfo;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment drive service.
/// </summary>
public class EnvironmentDriveService : IEnvironmentDriveService
{
    /// <inheritdoc />
    public IReadOnlyList<Models.DriveInfo> GetMountedDrives() =>
        IoDriveInfo
            .GetDrives()
            .Select(CreateFrom)
            .WhereNotNull()
            .ToArray();

    /// <inheritdoc />
    private static Models.DriveInfo CreateFrom(IoDriveInfo ioDriveInfo)
    {
        try
        {
            return new Models.DriveInfo
            {
                RootDirectory = ioDriveInfo.RootDirectory.FullName,
                Name = ioDriveInfo.Name,
                TotalSpaceBytes = ioDriveInfo.TotalSize,
                FreeSpaceBytes = ioDriveInfo.AvailableFreeSpace,
                DriveType = ioDriveInfo.DriveType,
                DriveFormat = ioDriveInfo.DriveFormat
            };
        }
        catch
        {
            return null;
        }
    }
}