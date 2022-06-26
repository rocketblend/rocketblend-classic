namespace RocketBlend.Services.Archives.Interfaces;

/// <summary>
/// The stream factory.
/// </summary>
public interface IStreamFactory
{
    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="inputStream">The input stream.</param>
    /// <returns>A Stream.</returns>
    Stream Create(Stream inputStream);
}