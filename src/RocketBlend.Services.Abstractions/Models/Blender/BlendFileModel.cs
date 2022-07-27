namespace RocketBlend.Services.Abstractions.Models.Blender;

/// <summary>
/// The blend file model.
/// </summary>
public class BlendFileModel : FileModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlendFileModel"/> class.
    /// </summary>
    public BlendFileModel()
    {
        this.Extension = ".blend";
    }
}
