using EasyCaching.Serialization.SystemTextJson.Configurations;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.Extensions.DependencyInjection;
using RocketBlend.Common.Caching.Interfaces;

namespace RocketBlend.Common.Caching;

/// <summary>
/// The cache extensions.
/// </summary>
public static class CachingExtensions
{
    /// <summary>
    /// Adds the redis cache.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        const string providerName = "default";
        services.AddEFSecondLevelCache(options =>
        {
            options.UseEasyCachingCoreProvider(providerName, isHybridCache: false).DisableLogging(true).UseCacheKeyPrefix("EF_");
            options.CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30));
        });

        // More info: https://easycaching.readthedocs.io/en/latest/Redis/
        services.AddEasyCaching(options =>
        {
            options.WithSystemTextJson();
            options.UseInMemory(providerName);
        });

        services.AddScoped<ICache, Cache>();

        return services;
    }
}