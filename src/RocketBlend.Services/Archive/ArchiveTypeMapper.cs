using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Configuration;

namespace RocketBlend.Services.Archive;

public class ArchiveTypeMapper : IArchiveTypeMapper
{
    private readonly IPathService _pathService;
    private readonly ArchiveTypeMapperConfiguration _configuration;

    public ArchiveTypeMapper(
        IPathService pathService,
        ArchiveTypeMapperConfiguration configuration)
    {
        this._pathService = pathService;
        this._configuration = configuration;
    }

    public ArchiveType? GetArchiveTypeFrom(string filePath)
    {
        var fileName = this._pathService.GetFileNameWithoutExtension(filePath);
        var extension = this._pathService.GetExtension(filePath);
        if (fileName.EndsWith(".tar"))
        {
            extension = "tar." + extension;
        }

        return this._configuration.ExtensionToArchiveTypeDictionary.TryGetValue(extension, out var result)
            ? result
            : (ArchiveType?) null;
    }
}