using CICDTemplate.Application.States.Commands.SaveState;

using Dapr.Client;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.SaveState;

public class SaveStateCommandHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<SaveStateCommandHandler> _logger = Substitute.For<ILogger<SaveStateCommandHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        SaveStateCommand command = new("Cookies", "Yummy!");
        SaveStateCommandHandler handler = new(_daprClient, _logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await _daprClient
            .Received(1)
            .SaveStateAsync(
            "statestore",
            "Cookies",
            command,
            null,
            null,
            Arg.Any<CancellationToken>());
    }
}