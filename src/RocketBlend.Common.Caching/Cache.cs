using System.Text.Json;
using EasyCaching.Core;
using RocketBlend.Common.Caching.Interfaces;

namespace RocketBlend.Common.Caching;

/// <summary>
/// The cache.
/// </summary>
public class Cache : ICache
{
    private readonly IEasyCachingProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cache"/> class.
    /// </summary>
    /// <param name="factory">The factory.</param>
    public Cache(IEasyCachingProviderFactory factory)
    {
        this._provider = factory.GetCachingProvider("Default");
    }

    /// <inheritdoc />
    public void Add(string key, object item, TimeSpan timeSpan)
    {
        this._provider.Set(key, JsonSerializer.Serialize(item), timeSpan);
    }

    /// <inheritdoc />
    public object Get(string key)
    {
        return this._provider.Get<object>(key);
    }

    /// <inheritdoc />
    public void Remove(string key)
    {
        this._provider.Remove(key);
    }
}
