using System.Reactive;
using Akavache;
using Newtonsoft.Json;
using ReactiveUI;
using Splat;

namespace RocketBlend.Presentation.Infrastructure;

/// <summary>
/// The akavache suspension driver.
/// </summary>
public class AkavacheSuspensionDriver<TAppState> : ISuspensionDriver
    where TAppState : class
{
    /// <summary>
    /// The key.
    /// </summary>
    private const string Key = "rocketblend-state";

    /// <summary>
    /// Initializes a new instance of the <see cref="AkavacheSuspensionDriver"/> class.
    /// </summary>
    static AkavacheSuspensionDriver() => Locator.CurrentMutable.RegisterConstant(new JsonSerializerSettings
    {
        ObjectCreationHandling = ObjectCreationHandling.Replace
    });

    /// <summary>
    /// Initializes a new instance of the <see cref="AkavacheSuspensionDriver"/> class.
    /// </summary>
    /// <param name="appName">The app name.</param>
    public AkavacheSuspensionDriver(string appName = "RocketBlendV1") => BlobCache.ApplicationName = appName;

    /// <inheritdoc />
    public IObservable<Unit> InvalidateState() => BlobCache.UserAccount.InvalidateObject<TAppState>(Key);

    /// <inheritdoc />
    public IObservable<object> LoadState() => BlobCache.UserAccount.GetObject<TAppState>(Key);

    /// <inheritdoc />
    public IObservable<Unit> SaveState(object state) => BlobCache.UserAccount.InsertObject(Key, (TAppState)state);
}