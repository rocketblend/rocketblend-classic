using FluentValidation;
using MediatR;
using ValidationException = RocketBlend.Common.Application.Exceptions.ValidationException;

namespace RocketBlend.Common.Application.Behaviours;

/// <summary>
/// The validation behaviour.
/// </summary>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehaviour"/> class.
    /// </summary>
    /// <param name="validators">The validators.</param>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        this._validators = validators;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (this._validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                this._validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);
        }
        return await next();
    }
}
