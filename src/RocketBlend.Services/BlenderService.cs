using RocketBlend.Services.Abstractions;

namespace RocketBlend.Services;

/// <summary>
/// The blender service.
/// </summary>
public class BlenderService : IBlenderService
{
    private readonly IResourceOpeningService _resourceOpeningService;
    private readonly IFileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderService"/> class.
    /// </summary>
    /// <param name="resourceOpeningService">The resource opening service.</param>
    /// <param name="fileService">The file service.</param>
    public BlenderService(
        IResourceOpeningService resourceOpeningService,
        IFileService fileService)
    {
        this._resourceOpeningService = resourceOpeningService;
        this._fileService = fileService;
    }

    /// <inheritdoc />
    public void Open(string executable, string args = "")
    {
        if (!this.IsValidBlenderExecutable(executable))
        {
            return;
        }

        this._resourceOpeningService.Open(executable);
    }

    /// <inheritdoc />
    public void Install(string path, Uri sourceUri)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void OpenBlendWith(string executable, string blendFile, string args = "")
    {
        if (!this.IsValidBlenderExecutable(executable) && !this.IsValidBlendFile(blendFile))
        {
            return;
        }

        this._resourceOpeningService.OpenWith(executable, "{0}", blendFile);
    }

    /// <inheritdoc />
    public void RenderWith(string executable, string blendFile, string args = "")
    {
        if (!this.IsValidBlenderExecutable(executable) && !this.IsValidBlendFile(blendFile))
        {
            return;
        }

        this._resourceOpeningService.OpenWith(executable, "-b {0} -f 1')", blendFile);
    }

    /// <inheritdoc />
    public void RenderAnimationWith(string executable, string blendFile, string args = "")
    {
        if (!this.IsValidBlenderExecutable(executable) && !this.IsValidBlendFile(blendFile))
        {
            return;
        }

        this._resourceOpeningService.OpenWith(executable, "-b {0} -a')", blendFile);
    }

    /// <inheritdoc />
    public void CreateBlendFileWith(string executable, string path)
    {
        if (!this.IsValidBlenderExecutable(executable) && this.IsValidBlendFile(path))
        {
            return;
        }

        this._resourceOpeningService.OpenWith(executable, "-b --python-expr import bpy;bpy.ops.wm.save_as_mainfile(filepath=r'{0}')", path);
    }

    /// <summary>
    /// Is the file a blender executable.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A bool.</returns>
    private bool IsValidBlenderExecutable(string file) => this._fileService.CheckIfExists(file);

    /// <summary>
    /// Is the file a blend file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>A bool.</returns>
    private bool IsValidBlendFile(string file) =>
        this._fileService.CheckIfExtension(file, ".blend") && this._fileService.CheckIfExists(file);
}