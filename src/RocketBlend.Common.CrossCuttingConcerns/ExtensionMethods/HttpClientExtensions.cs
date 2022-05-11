using System.Net.Http.Headers;
using System.Text;

namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The http client extensions.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Uses the basic authentication.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="password">The password.</param>
    public static void UseBasicAuthentication(this HttpClient client, string userName, string password)
    {
        var byteArray = Encoding.UTF8.GetBytes(userName + ":" + password);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }

    /// <summary>
    /// Uses the bearer token.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="token">The token.</param>
    public static void UseBearerToken(this HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
