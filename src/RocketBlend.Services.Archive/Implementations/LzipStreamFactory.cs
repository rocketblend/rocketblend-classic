using RocketBlend.Services.Archives.Interfaces;
using SharpCompress.Compressors;
using SharpCompress.Compressors.LZMA;

namespace RocketBlend.Services.Archives.Implementations;

/// <summary>
/// The lzip stream factory.
/// </summary>
public class LzipStreamFactory : IStreamFactory
{
    /// <inheritdoc />
    public Stream Create(Stream inputStream) => new LZipStream(inputStream, CompressionMode.Decompress);
}