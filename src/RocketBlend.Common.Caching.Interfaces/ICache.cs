namespace RocketBlend.Common.Caching.Interfaces;

/// <summary>
/// The cache.
/// </summary>
public interface ICache
{
    /// <summary>
    /// Adds the.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="item">The item.</param>
    /// <param name="timeSpan">The time span.</param>
    void Add(string key, object item, TimeSpan timeSpan);

    /// <summary>
    /// Removes the.
    /// </summary>
    /// <param name="key">The key.</param>
    void Remove(string key);

    /// <summary>
    /// Gets the.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>An object.</returns>
    object Get(string key);
}