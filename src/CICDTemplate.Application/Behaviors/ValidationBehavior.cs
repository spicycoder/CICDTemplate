﻿using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace CICDTemplate.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (next == null)
        {
            throw new ArgumentNullException(nameof(next), "The 'next' parameter cannot be null.");
        }

        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationFailure[] validationErrors = validators
            .Select(x => x.Validate(context))
            .Where(x => x.Errors.Count > 0)
            .SelectMany(x => x.Errors)
            .ToArray();

        if (validationErrors.Length > 0)
        {
            throw new ValidationException(validationErrors);
        }

        return await next();
    }
}