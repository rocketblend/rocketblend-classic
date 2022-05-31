using RocketBlend.Services.Environment.Enums;

namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The platform service.
/// </summary>
public interface IPlatformService
{
    /// <summary>
    /// Gets the platform.
    /// </summary>
    /// <returns>A Platform.</returns>
    Platform GetPlatform();
}