using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RocketBlend.Common.Application.Behaviours;

/// <summary>
/// The performance behaviour.
/// </summary>
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceBehaviour"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUser">The current user.</param>
    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUser currentUser)
    {
        this._timer = new Stopwatch();

        this._logger = logger;
        this._currentUser = currentUser;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        this._timer.Start();

        var response = await next();

        this._timer.Stop();

        var elapsedMilliseconds = this._timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            Guid userId = this._currentUser.UserId;

            this._logger.LogWarning("RocketBlend Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                requestName, elapsedMilliseconds, userId, request);
        }

        return response;
    }
}
