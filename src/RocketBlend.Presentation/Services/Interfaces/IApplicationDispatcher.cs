namespace RocketBlend.Presentation.Services.Interfaces;

/// <summary>
/// The application dispatcher.
/// </summary>
public interface IApplicationDispatcher
{
    /// <summary>
    /// Dispatches the.
    /// </summary>
    /// <param name="action">The action.</param>
    void Dispatch(Action action);

    /// <summary>
    /// Dispatches the async.
    /// </summary>
    /// <param name="task">The task.</param>
    /// <returns>A Task.</returns>
    Task DispatchAsync(Func<Task> task);
}
