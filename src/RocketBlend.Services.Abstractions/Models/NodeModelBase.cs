namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The node model base.
/// </summary>
public class NodeModelBase
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full path.
    /// </summary>
    public string FullPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the created date time.
    /// </summary>
    public DateTime CreatedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the last modified date time.
    /// </summary>
    public DateTime LastModifiedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the last access date time.
    /// </summary>
    public DateTime LastAccessDateTime { get; set; }
}