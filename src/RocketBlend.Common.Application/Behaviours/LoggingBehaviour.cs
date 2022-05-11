using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace RocketBlend.Common.Application.Behaviours;

/// <summary>
/// The logging behaviour.
/// </summary>
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehaviour"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUser">The current user.</param>
    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUser currentUser)
    {
        this._logger = logger;
        this._currentUser = currentUser;
    }

    /// <inheritdoc />
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        this._logger.LogInformation("RocketBlend Request: {Name} {@UserId} {@Request}",
            typeof(TRequest).Name, this._currentUser.UserId, request);

        return Task.CompletedTask;
    }
}
