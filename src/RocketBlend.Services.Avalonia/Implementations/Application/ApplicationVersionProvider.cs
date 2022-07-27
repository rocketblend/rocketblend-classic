using Microsoft.Extensions.PlatformAbstractions;
using RocketBlend.Services.Abstractions.Applications;

namespace RocketBlend.Services.Avalonia.Implementations.Applications;

/// <summary>
/// The application version provider.
/// </summary>
public class ApplicationVersionProvider : IApplicationVersionProvider
{
    /// <inheritdoc />
    public string Version => PlatformServices.Default.Application.ApplicationVersion;
}