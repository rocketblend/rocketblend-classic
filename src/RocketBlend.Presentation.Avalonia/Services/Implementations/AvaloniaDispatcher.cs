using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using RocketBlend.Presentation.Services.Interfaces;

namespace RocketBlend.Presentation.Avalonia.Services.Implementations;

/// <summary>
/// The avalonia dispatcher.
/// </summary>
public class AvaloniaDispatcher : IApplicationDispatcher
{
    /// <inheritdoc />
    private static Dispatcher Dispatcher => Dispatcher.UIThread;

    /// <inheritdoc />
    public void Dispatch(Action action) => Dispatcher.Post(action);

    /// <inheritdoc />
    public Task DispatchAsync(Func<Task> task) => Dispatcher.InvokeAsync(task);
}
