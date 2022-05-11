using RocketBlend.Common.Domain.Enums;

namespace RocketBlend.Common.Domain.Entities;

/// <summary>
/// The entity state.
/// </summary>
public interface IEntityState : IStateful<EntityState>
{
    /// <summary>
    /// Actives the entity.
    /// </summary>
    void Activate();

    /// <summary>
    /// Deactivates the entity.
    /// </summary>
    void Deactivate();

    /// <summary>
    /// Archives the entity.
    /// </summary>
    void Archive();
}
