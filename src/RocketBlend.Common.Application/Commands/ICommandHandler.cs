using MediatR;

namespace RocketBlend.Common.Application.Commands;

/// <summary>
/// The command handler.
/// </summary>
public interface ICommandHandler<in TCommand> :IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

/// <summary>
/// The command handler.
/// </summary>
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
