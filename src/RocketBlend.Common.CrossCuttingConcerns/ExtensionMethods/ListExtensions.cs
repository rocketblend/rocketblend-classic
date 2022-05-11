namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The list extensions.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Combines the.
    /// </summary>
    /// <param name="collection1">The collection1.</param>
    /// <param name="collection2">The collection2.</param>
    /// <returns>A list of TS.</returns>
    public static List<T> Combines<T>(this List<T> collection1, List<T> collection2)
    {
        collection1.AddRange(collection2);
        return collection1;
    }

    /// <summary>
    /// Combines the.
    /// </summary>
    /// <param name="collection1">The collection1.</param>
    /// <param name="collection2">The collection2.</param>
    /// <returns>An ICollection.</returns>
    public static ICollection<T> Combines<T>(this ICollection<T> collection1, ICollection<T> collection2)
    {
        var list = new List<T>();
        list.AddRange(collection1);
        list.AddRange(collection2);
        return list;
    }
}
