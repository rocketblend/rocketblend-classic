using System.Collections.Generic;

namespace RocketBlend.Presentation.Avalonia.Configuration;

/// <summary>
/// The languages configuration.
/// </summary>
public class LanguagesConfiguration
{
    /// <summary>
    /// Gets or sets the available locales.
    /// </summary>
    public List<string> AvailableLocales { get; set; } = new();
}
