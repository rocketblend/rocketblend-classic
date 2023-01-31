using System.Reflection;
using H.Pipes;
using Polly;
using Polly.Timeout;
using RocketBlend.Launcher.Core;

namespace RocketBlend.Launcher;

/// <summary>
/// The program.
/// </summary>
static class Program
{
    /// <summary>
    /// The app executable.
    /// </summary>
    private const string AppExecutable = "RocketBlend.Presentation.Avalonia.exe";

    /// <summary>
    /// Mains the.
    /// </summary>
    /// <param name="args">The args.</param>
    static async Task Main(string[] args)
    {
        bool isApplicationOpen = await PipeArgsToApplication(args);

        if (!isApplicationOpen)
        {
            LaunchApplicationWithArgs(args);
        }
    }

    /// <summary>
    /// Launches the application with args.
    /// </summary>
    /// <param name="args">The args.</param>
    private static void LaunchApplicationWithArgs(string[] args)
    {
        string myPath = Assembly.GetEntryAssembly()!.Location;
        string myDir = Path.GetDirectoryName(myPath)!;
        string path = Path.Combine(myDir, AppExecutable);

        var exists = File.Exists(path);
        if (exists)
        {
            System.Diagnostics.Process.Start(path, args);
        }
    }

    /// <summary>
    /// Pipes the args to application.
    /// </summary>
    /// <param name="args">The args.</param>
    /// <returns>A Task.</returns>
    private static async Task<bool> PipeArgsToApplication(string[] args)
    {
        bool success = false;
        
        try
        {
            using var source = new CancellationTokenSource();
            await using var client = new PipeClient<string[]>(Pipes.Default);

            client.MessageReceived += (o, args) => Console.WriteLine("MessageReceived: " + args.Message);
            client.Disconnected += (o, args) => Console.WriteLine("Disconnected from server");
            client.Connected += (o, args) => Console.WriteLine("Connected to server");
            client.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception.Message);

            var policy = Policy.WrapAsync(
                Policy
                    .Handle<InvalidOperationException>()
                    .RetryAsync(1, async (exception, count) =>
                    {
                        await client.ConnectAsync();
                    }),
                Policy.TimeoutAsync(TimeSpan.FromMilliseconds(500), TimeoutStrategy.Optimistic));

            await policy.ExecuteAsync(async cancellationToken =>
            {
                await client.WriteAsync(args, cancellationToken).ConfigureAwait(false);
                success = true;
            }, source.Token);

            await client.ConnectAsync(source.Token).ConfigureAwait(false);
        }
        catch (Exception)
        {
        }

        return success;
    }

    /// <summary>
    /// Ons the exception occurred.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    static void OnExceptionOccurred(string errorMessage)
    {
        ErrorToConsole(new List<string>() { errorMessage });
    }

    /// <summary>
    /// Errors the to console.
    /// </summary>
    /// <param name="errorMessages">The error messages.</param>
    static void ErrorToConsole(IEnumerable<string> errorMessages)
    {
        TextWriter errorWriter = Console.Error;
        foreach (var errorMessage in errorMessages)
        {
            errorWriter.WriteLine(errorMessage);
        }
    }
}
