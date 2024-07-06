using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CICDTemplate.Application.Secrets.Queries;

public sealed class ReadSecretHandler(
    DaprClient daprClient,
    ILogger<ReadSecretHandler> logger) : IRequestHandler<ReadSecretCommand, string?>
{
    public async Task<string?> Handle(
        ReadSecretCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var secret = await daprClient.GetSecretAsync(Constants.SecretsStoreName, request.Key, null, cancellationToken);
        if (!secret.TryGetValue(request.Key, out var value))
        {
            logger.LogError("Secret not found for Key: {Key}", request.Key);
            return null;
        }
        logger.LogInformation("Secret found for {Key}: {Value}", request.Key, value);
        return value;
    }
}
