using RocketBlend.Services.Archives.Interfaces;
using SharpCompress.Compressors.Xz;

namespace RocketBlend.Services.Archives.Implementations;

/// <summary>
/// The xz stream factory.
/// </summary>
public class XzStreamFactory : IStreamFactory
{
    /// <inheritdoc />
    public Stream Create(Stream inputStream) => new XZStream(inputStream);
}