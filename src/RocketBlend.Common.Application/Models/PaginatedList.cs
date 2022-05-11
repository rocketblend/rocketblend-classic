namespace RocketBlend.Common.Application.Models;

/// <summary>
/// The paginated list.
/// </summary>
public class PaginatedList<T>
{
    /// <summary>
    /// Gets the items.
    /// </summary>
    public List<T> Items { get; }

    /// <summary>
    /// Gets the page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Gets the total count.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList"/> class.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="count">The count.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber;
        this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        this.TotalCount = count;
        this.Items = items;
    }

    /// <summary>
    /// Gets a value indicating whether has previous page.
    /// </summary>
    public bool HasPreviousPage => this.PageNumber > 1;

    /// <summary>
    /// Gets a value indicating whether has next page.
    /// </summary>
    public bool HasNextPage => this.PageNumber < this.TotalPages;

    /// <summary>
    /// Creates the.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A list of TS.</returns>
    public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
