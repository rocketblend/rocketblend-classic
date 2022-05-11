using RocketBlend.Common.CrossCuttingConcerns.OS;
using RocketBlend.Infrastructure.OS;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The date time provider extensions.
/// </summary>
public static class DateTimeProviderExtensions
{
    /// <summary>
    /// Adds the date time provider.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}