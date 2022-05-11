using AutoMapper;
using AutoMapper.QueryableExtensions;
using RocketBlend.Common.Application.Models;

namespace RocketBlend.Common.Application.Mappings;

/// <summary>
/// The mapping extensions.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Tos the paginated list.
    /// </summary>
    /// <param name="queryable">The queryable.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A list of TDestinations.</returns>
    public static PaginatedList<TDestination> ToPaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        => PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);

    /// <summary>
    /// Projects the to list.
    /// </summary>
    /// <param name="queryable">The queryable.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A list of TDestinations.</returns>
    public static List<TDestination> ProjectToList<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
        => queryable.ProjectTo<TDestination>(configuration).ToList();
}
