using System.Text.Json;

namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The object extensions.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// As the json string.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>A string.</returns>
    public static string AsJsonString(this object obj)
    {
        var content = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        return content;
    }
}
