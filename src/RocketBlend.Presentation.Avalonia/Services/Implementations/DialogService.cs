using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia;
using RocketBlend.Avalonia.Interfaces;
using RocketBlend.Presentation.Extensions;
using RocketBlend.Presentation.Avalonia.Views;
using RocketBlend.Presentation.Avalonia.Views.Dialogs;
using RocketBlend.Presentation.Services;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Presentation.ViewModels;
using RocketBlend.Presentation.ViewModels.Dialogs;
using Splat;

namespace RocketBlend.Presentation.Avalonia.Services.Implementations;

/// <summary>
/// The dialog service.
/// </summary>
public class DialogService : IDialogService
{
    private readonly IMainWindowProvider _mainWindowProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogService"/> class.
    /// </summary>
    /// <param name="mainWindowProvider">The main window provider.</param>
    public DialogService(IMainWindowProvider mainWindowProvider)
    {
        this._mainWindowProvider = mainWindowProvider;
    }

    /// <inheritdoc />
    public async Task<TResult> ShowDialogAsync<TResult>(string viewModelName)
        where TResult : DialogResultBase
    {
        var window = CreateView<TResult>(viewModelName);
        var viewModel = CreateViewModel<TResult>(viewModelName);
        Bind(window, viewModel);

        return await this.ShowDialogAsync(window);
    }

    /// <inheritdoc />
    public async Task<TResult> ShowDialogAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
        where TResult : DialogResultBase
        where TParameter : NavigationParameterBase
    {
        var window = CreateView<TResult>(viewModelName);
        var viewModel = CreateViewModel<TResult>(viewModelName);
        Bind(window, viewModel);

        switch (viewModel)
        {
            case ParameterizedDialogViewModelBase<TResult, TParameter> parameterizedDialogViewModelBase:
                parameterizedDialogViewModelBase.Activate(parameter);
                break;
            case ParameterizedDialogViewModelBaseAsync<TResult, TParameter> parameterizedDialogViewModelBaseAsync:
                await parameterizedDialogViewModelBaseAsync.ActivateAsync(parameter);
                break;
            default:
                throw new InvalidOperationException(
                    $"{viewModel.GetType().FullName} doesn't support passing parameters!");
        }

        return await this.ShowDialogAsync(window);
    }

    /// <inheritdoc />
    public Task ShowDialogAsync(string viewModelName) => ShowDialogAsync<DialogResultBase>(viewModelName);

    /// <inheritdoc />
    public Task ShowDialogAsync<TParameter>(string viewModelName, TParameter parameter)
        where TParameter : NavigationParameterBase =>
        ShowDialogAsync<DialogResultBase, TParameter>(viewModelName, parameter);


    /// <summary>
    /// Binds the.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <param name="viewModel">The view model.</param>
    private static void Bind(IDataContextProvider window, object viewModel) => window.DataContext = viewModel;

    /// <summary>
    /// Creates the view.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <returns>A DialogWindowBase.</returns>
    private static DialogWindowBase<TResult> CreateView<TResult>(string viewModelName)
        where TResult : DialogResultBase
    {
        var viewType = GetViewType(viewModelName);
        if (viewType is null)
        {
            throw new InvalidOperationException($"View for {viewModelName} was not found!");
        }

        return (DialogWindowBase<TResult>)GetView(viewType);
    }

    /// <summary>
    /// Creates the view model.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <returns>A DialogViewModelBase.</returns>
    private static DialogViewModelBase<TResult> CreateViewModel<TResult>(string viewModelName)
        where TResult : DialogResultBase
    {
        var viewModelType = GetViewModelType(viewModelName);
        return viewModelType is null
            ? throw new InvalidOperationException($"View model {viewModelName} was not found!")
            : (DialogViewModelBase<TResult>)GetViewModel(viewModelType);
    }

    /// <summary>
    /// Gets the view model type.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <returns>A Type.</returns>
    private static Type GetViewModelType(string viewModelName)
    {
        var viewModelsAssembly = Assembly.GetAssembly(typeof(ViewModelBase));
        if (viewModelsAssembly is null)
        {
            throw new InvalidOperationException("Broken installation!");
        }

        var viewModelTypes = viewModelsAssembly.GetTypes();

        return viewModelTypes.SingleOrDefault(t => t.Name == viewModelName);
    }

    /// <summary>
    /// Gets the view.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>An object.</returns>
    private static object GetView(Type type) => Activator.CreateInstance(type);

    /// <summary>
    /// Gets the view model.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>An object.</returns>
    private static object GetViewModel(Type type) => Locator.Current.GetRequiredService(type);

    /// <summary>
    /// Gets the view type.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <returns>A Type.</returns>
    private static Type GetViewType(string viewModelName)
    {
        var viewsAssembly = Assembly.GetExecutingAssembly();
        var viewTypes = viewsAssembly.GetTypes();
        var viewName = viewModelName.Replace("ViewModel", string.Empty);

        return viewTypes.SingleOrDefault(t => t.Name == viewName);
    }

    /// <summary>
    /// Shows the dialog async.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns>A Task.</returns>
    private async Task<TResult> ShowDialogAsync<TResult>(DialogWindowBase<TResult> window)
        where TResult : DialogResultBase
    {
        var mainWindow = (MainWindow)this._mainWindowProvider.GetMainWindow();

        //mainWindow.ShowOverlay();
        var result = await window.ShowDialog<TResult>(mainWindow);
        //mainWindow.HideOverlay();
        if (window is IDisposable disposable)
        {
            disposable.Dispose();
        }

        return result;
    }
}
