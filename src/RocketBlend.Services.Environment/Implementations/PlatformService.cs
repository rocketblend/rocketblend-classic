using System.Runtime.InteropServices;
using RocketBlend.Services.Environment.Enums;
using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The platform service.
/// </summary>
public class PlatformService : IPlatformService
{
    /// <inheritdoc />
    public Platform GetPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Platform.Linux;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Platform.MacOs;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Platform.Windows;
        }

        return Platform.Unknown;
    }
}