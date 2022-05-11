using Splat;

namespace RocketBlend.Core.Utils;

/// <summary>
/// The readonly dependency resolver extensions.
/// Possibly don't need this and can just fully use MS one.
/// </summary>
public static class ReadonlyDependencyResolverExtensions
{
    /// <summary>
    /// Gets the required service.
    /// </summary>
    /// <param name="resolver">The resolver.</param>
    /// <returns>A TService.</returns>
    public static TService GetRequiredService<TService>(this IReadonlyDependencyResolver resolver)
    {
        var service = resolver.GetService<TService>();
        if (service is null)
        {
            throw new InvalidOperationException($"Failed to resolve object of type {typeof(TService)}");
        }

        return service;
    }

    /// <summary>
    /// Gets the required service.
    /// </summary>
    /// <param name="resolver">The resolver.</param>
    /// <param name="type">The type.</param>
    /// <returns>An object.</returns>
    public static object GetRequiredService(this IReadonlyDependencyResolver resolver, Type type)
    {
        var service = resolver.GetService(type);
        if (service is null)
        {
            throw new InvalidOperationException($"Failed to resolve object of type {type}");
        }

        return service;
    }
}