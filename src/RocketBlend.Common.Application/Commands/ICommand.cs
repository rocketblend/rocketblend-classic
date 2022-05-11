using MediatR;

namespace RocketBlend.Common.Application.Commands;

/// <summary>
/// The command.
/// </summary>
public interface ICommand : IRequest
{
}

/// <summary>
/// The command.
/// </summary>
public interface ICommand<out TResult> : IRequest<TResult>
{
}
