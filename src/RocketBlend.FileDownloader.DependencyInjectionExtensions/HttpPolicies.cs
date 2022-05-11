using Polly;
using Polly.Extensions.Http;

namespace RocketBlend.FileDownloader.DependencyInjectionExtensions;

/// <summary>
/// The http policies.
/// </summary>
public static class HttpPolicies
{
    /// <summary>
    /// Gets the retry policy.
    /// </summary>
    /// <returns>An IAsyncPolicy.</returns>
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    /// <summary>
    /// Gets the circuit breaker policy.
    /// </summary>
    /// <returns>An IAsyncPolicy.</returns>
    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
