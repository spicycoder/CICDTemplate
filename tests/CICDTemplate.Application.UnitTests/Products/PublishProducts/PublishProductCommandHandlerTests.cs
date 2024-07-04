using CICDTemplate.Application.Products.Commands.PublishProduct;
using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Products.PublishProducts;

public class PublishProductCommandHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<PublishProductCommandHandler> _logger = Substitute.For<ILogger<PublishProductCommandHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        PublishProductCommand command = new("Cookies", "Yummy!");

        PublishProductCommandHandler handler = new(_daprClient, _logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await _daprClient
            .Received(1)
            .PublishEventAsync(
            Constants.PubSubName,
            Constants.PubSubTopicName,
            command,
            Arg.Any<CancellationToken>());
    }
}
