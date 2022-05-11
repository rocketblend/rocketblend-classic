using MediatR;
using Microsoft.Extensions.Logging;

namespace RocketBlend.Common.Application.Behaviours;

/// <summary>
/// Unhandled exception behaviour.
/// </summary>
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnhandledExceptionBehaviour"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        this._logger = logger;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            this._logger.LogError(ex, "RocketBlend Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
