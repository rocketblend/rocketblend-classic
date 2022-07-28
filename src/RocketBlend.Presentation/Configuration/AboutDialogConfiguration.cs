namespace RocketBlend.Presentation.Configuration;

/// <summary>
/// The about dialog configuration.
/// </summary>
public class AboutDialogConfiguration
{
    /// <summary>
    /// Gets or sets the website URL.
    /// </summary>
    public string WebsiteUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the credits.
    /// </summary>
    public string[] Credits { get; set; } = System.Array.Empty<string>();
}