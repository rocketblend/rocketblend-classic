namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// Has key interface.
/// </summary>
public interface IHasKey<TKey>
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    TKey Id { get; }
}