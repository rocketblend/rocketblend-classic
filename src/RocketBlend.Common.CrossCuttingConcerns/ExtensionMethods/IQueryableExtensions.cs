namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The i queryable extensions.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Paginates list.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>An IQueryable.</returns>
    public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page, int pageSize)
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
