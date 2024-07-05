using CICDTemplate.Application.States.Queries.ReadState;

using Dapr.Client;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Products.ReadState;

public class ReadStateCommandTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<ReadStateCommandHandler> _logger = Substitute.For<ILogger<ReadStateCommandHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        ReadStateCommand command = new("Cookies");
        var handler = new ReadStateCommandHandler(_daprClient, _logger);
        _daprClient
            .GetStateAsync<ProductState?>(
            "statestore",
            "Cookies",
            null,
            null,
            CancellationToken.None)
            .Returns(new ProductState("Cookies", "Yummy!"));

        // Act
        ProductState? state = await handler.Handle(command, CancellationToken.None);

        // Assert
        await _daprClient
            .Received(1)
            .GetStateAsync<ProductState?>(
            "statestore",
            "Cookies",
            null,
            null,
            Arg.Any<CancellationToken>());

        state.Should().NotBeNull();
        state!.Name.Should().Be("Cookies");
        state!.Description.Should().Be("Yummy!");
    }
}
