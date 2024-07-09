using CICDTemplate.Application.Products.Commands.DeleteState;

using Dapr.Client;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Products.DeleteState;

public class DeleteStateCommandHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<DeleteStateCommandHandler> _logger = Substitute.For<ILogger<DeleteStateCommandHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        DeleteStateCommand command = new("Cookies");
        DeleteStateCommandHandler handler = new(_daprClient, _logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await _daprClient
            .Received(1)
            .DeleteStateAsync(
            "statestore",
            Arg.Any<string>(),
            null,
            null,
            Arg.Any<CancellationToken>());
    }
}