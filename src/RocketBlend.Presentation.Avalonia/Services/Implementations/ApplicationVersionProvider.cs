using Microsoft.Extensions.PlatformAbstractions;
using RocketBlend.Presentation.Services.Interfaces;

namespace RocketBlend.Presentation.Avalonia.Services.Implementations;

/// <summary>
/// The application version provider.
/// </summary>
public class ApplicationVersionProvider : IApplicationVersionProvider
{
    /// <inheritdoc />
    public string Version => PlatformServices.Default.Application.ApplicationVersion;
}