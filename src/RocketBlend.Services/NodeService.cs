using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services;

public class NodeService : INodeService
{
    private readonly IFileService _fileService;
    private readonly IDirectoryService _directoryService;

    public NodeService(
        IFileService fileService,
        IDirectoryService directoryService)
    {
        this._fileService = fileService;
        this._directoryService = directoryService;
    }

    public bool CheckIfExists(string nodePath) =>
        this._fileService.CheckIfExists(nodePath) || this._directoryService.CheckIfExists(nodePath);

    public NodeModelBase GetNode(string nodePath) =>
        this._fileService.CheckIfExists(nodePath)
            ? this._fileService.GetFile(nodePath)
            : this._directoryService.GetDirectory(nodePath);
}