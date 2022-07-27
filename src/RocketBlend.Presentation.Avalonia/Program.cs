using Avalonia;
using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.ReactiveUI;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Serilog;
using Splat;
using System;
using System.Runtime.InteropServices;

namespace RocketBlend.Presentation.Avalonia;

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
        try
        {
            RegisterDependencies();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Something very bad happened");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    /// <summary>
    /// Builds the avalonia app.
    /// </summary>
    /// <returns>An AppBuilder.</returns>
    private static AppBuilder BuildAvaloniaApp()
    {
        return BuildAvaloniaApp(false);
    }

    /// <summary>
    /// Builds the avalonia app.
    /// </summary>
    /// <param name="startInBg">If true, start in bg.</param>
    /// <returns>An AppBuilder.</returns>
    private static AppBuilder BuildAvaloniaApp(bool startInBg)
    {
        bool useGpuLinux = true;

        var result = AppBuilder.Configure(() => new App(() => System.Threading.Tasks.Task.CompletedTask, startInBg))
            .UseReactiveUI()
            .LogToTrace()
            .WithIcons(container => container
                .Register<FontAwesomeIconProvider>());

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            result
                .UseWin32()
                .UseSkia();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            result.UsePlatformDetect()
                .UseManagedSystemDialogs<AppBuilder, Window>();
        }
        else
        {
            result.UsePlatformDetect();
        }

        return result
            .With(new Win32PlatformOptions { AllowEglInitialization = true, UseDeferredRendering = true, UseWindowsUIComposition = true })
            .With(new X11PlatformOptions { UseGpu = useGpuLinux, WmClass = "RocketBlend" })
            .With(new AvaloniaNativePlatformOptions { UseDeferredRendering = true, UseGpu = true })
            .With(new MacOSPlatformOptions { ShowInDock = true });
    }

    /// <summary>
    /// Registers the dependencies.
    /// </summary>
    private static void RegisterDependencies() =>
        Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);
}
