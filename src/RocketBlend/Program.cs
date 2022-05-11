using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.DependencyInjection;
using RocketBlend.Views;
using Splat;
using System;
using System.Reflection;

namespace RocketBlend;

/// <summary>
/// The program.
/// </summary>
internal class Program
{

    /// <summary>
    /// Initialization code. Don't use any Avalonia, third-party APIs or any
    /// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    /// yet and stuff might break.
    /// </summary>
    /// <param name="args">The args.</param>
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    } 

    /// <summary>
    /// Builds the avalonia app.
    /// Avalonia configuration, don't remove; also used by visual designer.
    /// </summary>
    /// <returns>An AppBuilder.</returns>
    public static AppBuilder BuildAvaloniaApp()
    {
        var result = AppBuilder.Configure<App>()
                  .UsePlatformDetect()
                  .LogToTrace()
                  .UseReactiveUI();

        return result;
    }
}
