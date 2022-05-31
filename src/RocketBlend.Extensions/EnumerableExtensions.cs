using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketBlend.Extensions;

/// <summary>
/// The enumerable extensions.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Fors the each.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="action">The action.</param>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
        {
            action(item);
        }
    }

    /// <summary>
    /// Fors the each async.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="action">The action.</param>
    /// <returns>A Task.</returns>
    public static async Task ForEachAsync<T>(this IEnumerable<T> collection, Func<T, Task> action)
    {
        foreach (var item in collection)
        {
            await action(item);
        }
    }

    /// <summary>
    /// Wheres the not null.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>A list of TS.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> collection) =>
        collection.Where(t => t is not null);
}