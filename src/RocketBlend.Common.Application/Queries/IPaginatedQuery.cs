namespace RocketBlend.Common.Application.Queries;

/// <summary>
/// Paginated query interface.
/// </summary>
public interface IPaginatedQuery
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; }
}
