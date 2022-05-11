namespace RocketBlend.Common.Domain.Entities;

/// <summary>
/// The stateful.
/// </summary>
public interface IStateful<TState>
    where TState : Enum
{
    /// <summary>
    /// Gets the state.
    /// </summary>
    TState State { get; }
}
