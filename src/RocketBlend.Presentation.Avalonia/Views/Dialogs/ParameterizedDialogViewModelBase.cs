using System.Threading;
using System.Threading.Tasks;
using RocketBlend.Presentation.Services;
using RocketBlend.Presentation.ViewModels.Dialogs;

namespace RocketBlend.Presentation.Avalonia.Views.Dialogs;

/// <summary>
/// The parameterized dialog view model base.
/// </summary>
public abstract class ParameterizedDialogViewModelBase<TResult, TParameter> : DialogViewModelBase<TResult>
    where TResult : DialogResultBase
    where TParameter : NavigationParameterBase
{
    /// <summary>
    /// Activates the.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    public abstract void Activate(TParameter parameter);
}

/// <summary>
/// The parameterized dialog view model base async.
/// </summary>
public abstract class ParameterizedDialogViewModelBaseAsync<TResult, TParameter> : DialogViewModelBase<TResult>
    where TResult : DialogResultBase
    where TParameter : NavigationParameterBase
{
    /// <summary>
    /// Activates the async.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public abstract Task ActivateAsync(TParameter parameter, CancellationToken cancellationToken = default);
}

/// <summary>
/// The parameterized dialog view model base.
/// </summary>
public abstract class ParameterizedDialogViewModelBase<TParameter> : ParameterizedDialogViewModelBase<DialogResultBase, TParameter>
    where TParameter : NavigationParameterBase
{

}
