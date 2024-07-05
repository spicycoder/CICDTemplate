using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CICDTemplate.Application.States.Queries.ReadState;

public sealed class ReadStateCommandHandler(
    DaprClient daprClient,
    ILogger<ReadStateCommandHandler> logger) : IRequestHandler<ReadStateCommand, ProductState?>
{
    public async Task<ProductState?> Handle(ReadStateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ProductState? state = await daprClient.GetStateAsync<ProductState?>(
            Constants.StateStoreName,
            request.ProductName,
            cancellationToken: cancellationToken);

        logger.LogInformation("State: {State}", state);

        return state;
    }
}
