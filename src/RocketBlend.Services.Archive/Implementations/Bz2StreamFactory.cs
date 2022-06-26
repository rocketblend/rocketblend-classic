using RocketBlend.Services.Archives.Interfaces;
using SharpCompress.Compressors;
using SharpCompress.Compressors.BZip2;

namespace RocketBlend.Services.Archives.Implementations;

/// <summary>
/// The bz2 stream factory.
/// </summary>
public class Bz2StreamFactory : IStreamFactory
{
    /// <inheritdoc />
    public Stream Create(Stream inputStream) =>
        new BZip2Stream(inputStream, CompressionMode.Decompress, false);
}