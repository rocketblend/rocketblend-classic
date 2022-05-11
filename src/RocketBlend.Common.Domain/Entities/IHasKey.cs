using System.ComponentModel.DataAnnotations.Schema;

namespace RocketBlend.Common.Domain.Entities;

/// <summary>
/// Has key interface.
/// </summary>
public interface IHasKey<TKey>
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    TKey Id { get; }
}