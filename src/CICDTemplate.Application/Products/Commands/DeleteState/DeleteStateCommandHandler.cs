using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CICDTemplate.Application.Products.Commands.DeleteState;

public sealed class DeleteStateCommandHandler(
    DaprClient daprClient,
    ILogger<DeleteStateCommandHandler> logger) : IRequestHandler<DeleteStateCommand>
{
    public async Task Handle(DeleteStateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Deleting state: {ProductName}", request.productName);

        await daprClient.DeleteStateAsync(
            Constants.StateStoreName,
            request.productName,
            cancellationToken: cancellationToken);
    }
}