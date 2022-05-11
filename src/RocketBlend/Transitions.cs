using System;
using Avalonia.Animation;

namespace RocketBlend;

/// <summary>
/// Transitions.
/// </summary>
public static class Transitions
{
    /// <summary>
    /// Gets the fade transition.
    /// </summary>
    public static IPageTransition Fade => new CrossFade(TimeSpan.FromMilliseconds(85));

    /// <summary>
    /// Gets the slide transition.
    /// </summary>
    public static IPageTransition Slide => new PageSlide(TimeSpan.FromMilliseconds(200));
}
