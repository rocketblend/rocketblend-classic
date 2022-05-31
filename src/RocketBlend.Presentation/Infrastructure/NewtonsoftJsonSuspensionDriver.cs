using System.Reactive;
using System.Reactive.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReactiveUI;

namespace RocketBlend.Presentation.Infrastructure;

/// <summary>
/// The newtonsoft json suspension driver.
/// </summary>
public sealed class NewtonsoftJsonSuspensionDriver : ISuspensionDriver
{
    private readonly string _stateFilePath;
    private readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        NullValueHandling = NullValueHandling.Ignore,
        ObjectCreationHandling = ObjectCreationHandling.Replace,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="NewtonsoftJsonSuspensionDriver"/> class.
    /// </summary>
    /// <param name="stateFilePath">The state file path.</param>
    public NewtonsoftJsonSuspensionDriver(string stateFilePath) => this._stateFilePath = stateFilePath;

    /// <inheritdoc />
    public IObservable<Unit> InvalidateState()
    {
        if (File.Exists(this._stateFilePath))
            File.Delete(this._stateFilePath);
        return Observable.Return(Unit.Default);
    }

    /// <inheritdoc />
    public IObservable<object> LoadState()
    {
        if (!File.Exists(this._stateFilePath))
        {
            return Observable.Throw<object>(new FileNotFoundException(this._stateFilePath));
        }

        var lines = File.ReadAllText(this._stateFilePath);
        var state = JsonConvert.DeserializeObject<object>(lines, this._settings);
        return Observable.Return(state);
    }

    /// <inheritdoc />
    public IObservable<Unit> SaveState(object state)
    {
        var lines = JsonConvert.SerializeObject(state, Formatting.Indented, this._settings);
        File.WriteAllText(this._stateFilePath, lines);
        return Observable.Return(Unit.Default);
    }
}