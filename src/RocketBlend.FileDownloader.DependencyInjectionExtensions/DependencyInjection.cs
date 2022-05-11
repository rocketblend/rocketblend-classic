using Microsoft.Extensions.DependencyInjection;
using RocketBlend.FileDownloader.Interfaces;

namespace RocketBlend.FileDownloader.DependencyInjectionExtensions;

/// <summary>
/// Dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the file downloader service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddFileDownloaderService(this IServiceCollection services)
    {
        services.AddHttpClient("downloader")
            .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
            .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

        services.AddScoped<IFileDownloaderService, FileDownloaderService>();
        return services;
    }

    /// <summary>
    /// Adds the zip file extractor service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddZipFileExtractorService(this IServiceCollection services)
    {
        services.AddScoped<IFileExtractorService, ZipFileExtractorService>();
        return services;
    }
}
