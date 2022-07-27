namespace RocketBlend.Presentation.Models;

/// <summary>
/// The sort container.
/// </summary>
public sealed class SortContainer<T> : IEquatable<SortContainer<T>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SortContainer"/> class.
    /// </summary>
    /// <param name="description">The description.</param>
    /// <param name="comparer">The comparer.</param>
    public SortContainer(string description, IComparer<T> comparer)
    {
        this.Description = description;
        this.Comparer = comparer;
    }

    /// <summary>
    /// Gets the comparer.
    /// </summary>
    public IComparer<T> Comparer { get; }
    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; }

    #region Equality members

    /// <inheritdoc />
    public bool Equals(SortContainer<T>? other)
    {
        return other is not null && (ReferenceEquals(this, other) || string.Equals(this.Description, other.Description));
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is not null && (ReferenceEquals(this, obj) || (obj.GetType() == this.GetType() && this.Equals((SortContainer<T>)obj)));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (this.Description != null ? this.Description.GetHashCode() : 0);
    }

    public static bool operator ==(SortContainer<T> left, SortContainer<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SortContainer<T> left, SortContainer<T> right)
    {
        return !Equals(left, right);
    }

    #endregion
}