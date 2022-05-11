using System.Net.Http.Headers;
using System.Text.Json;

namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The http content extensions.
/// </summary>
public static class HttpContentExtensions
{
    /// <summary>
    /// Reads the as.
    /// </summary>
    /// <param name="httpContent">The http content.</param>
    /// <returns>A Task.</returns>
    public static async Task<T?> ReadAs<T>(this HttpContent httpContent)
    {
        var json = await httpContent.ReadAsStringAsync();

        return string.IsNullOrWhiteSpace(json)
            ? default
            : JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    /// <summary>
    /// As the string content.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="contentType">The content type.</param>
    /// <returns>A StringContent.</returns>
    public static StringContent AsStringContent(this object obj, string contentType)
    {
        var content = new StringContent(JsonSerializer.Serialize(obj));
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        return content;
    }

    /// <summary>
    /// As the json content.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>A StringContent.</returns>
    public static StringContent AsJsonContent(this object obj)
    {
        return obj.AsStringContent("application/json");
    }
}
