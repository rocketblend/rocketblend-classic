using RocketBlend.Services.Abstractions.Models.Dialogs;

namespace RocketBlend.Presentation.Services.Interfaces;

/// <summary>
/// The dialog service.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Shows the dialog async.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <returns>A Task.</returns>
    Task<TResult> ShowDialogAsync<TResult>(string viewModelName)
        where TResult : DialogResultBase;

    /// <summary>
    /// Shows the dialog async.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <returns>A Task.</returns>
    Task ShowDialogAsync(string viewModelName);

    /// <summary>
    /// Shows the dialog async.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>A Task.</returns>
    Task ShowDialogAsync<TParameter>(string viewModelName, TParameter parameter)
        where TParameter : NavigationParameterBase;

    /// <summary>
    /// Shows the dialog async.
    /// </summary>
    /// <param name="viewModelName">The view model name.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>A Task.</returns>
    Task<TResult> ShowDialogAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
        where TResult : DialogResultBase
        where TParameter : NavigationParameterBase;
}
