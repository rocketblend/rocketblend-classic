using System;
using System.Threading;

namespace RocketBlend.Extensions;

/// <summary>
/// The event extensions.
/// </summary>
public static class EventExtensions
{
    /// <summary>
    /// Raises the.
    /// </summary>
    /// <param name="eventHandler">The event handler.</param>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The args.</param>
    public static void Raise<TEventArgs>(
        this EventHandler<TEventArgs>? eventHandler,
        object sender,
        TEventArgs args) where TEventArgs : EventArgs
    {
        var handler = Volatile.Read(ref eventHandler);

        handler?.Invoke(sender, args);
    }
}