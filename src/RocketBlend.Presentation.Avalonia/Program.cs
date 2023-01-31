using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.ReactiveUI;
using CommandLine;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using RocketBlend.Launcher.Core.Options;
using RocketBlend.Launcher.Core.Options.Interfaces;
using RocketBlend.Presentation.Avalonia.Services;
using Serilog;
using Splat;

namespace RocketBlend.Presentation.Avalonia;

/// <summary>
/// The program.
/// </summary>
internal static class Program
{
    private static bool _startInBackground = false;

    /// <summary>
    /// Initialization code. Don't use any Avalonia, third-party APIs or any
    /// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    /// yet and stuff might break.
    /// </summary>
    /// <param name="args">The args.</param>
    [STAThread]
    public static void Main(string[] args)
    {
        var result = Parser.Default.ParseArguments<GeneralOptions>(args)
          .MapResult(
            options => RunAndReturnExitCode(options),
            _ => 0);

        if (result != 0)
        {
            Environment.Exit(result);
        }

        try
        {
            RegisterDependencies();
            SingleInstanceHelper.EnsureNotAlreadyRunning(() =>
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args));
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
    /// <param name="startInBg">If true, start in bg.</param>
    /// <returns>An AppBuilder.</returns>
    private static AppBuilder BuildAvaloniaApp()
    {
        bool useGpuLinux = true;

        var result = AppBuilder.Configure(() => new App(() => System.Threading.Tasks.Task.CompletedTask, _startInBackground))
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
    /// Runs the and return exit code.
    /// </summary>
    /// <param name="opts">The opts.</param>
    /// <returns>An int.</returns>
    private static int RunAndReturnExitCode(IGeneralOptions opts)
    {
        _startInBackground = opts.StartInBackground;
        return 0;
    }

    /// <summary>
    /// Registers the dependencies.
    /// </summary>
    private static void RegisterDependencies() =>
        Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);
}