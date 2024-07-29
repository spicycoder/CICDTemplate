using CICDTemplate.Application.States.Queries.ReadState;

using Dapr.Client;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.ReadState;

public class ReadStateQueryHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<ReadStateQueryHandler> _logger = Substitute.For<ILogger<ReadStateQueryHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        ReadStateQuery query = new("Cookies");
        var handler = new ReadStateQueryHandler(_daprClient, _logger);
        _daprClient
            .GetStateAsync<ProductState?>(
            "statestore",
            "Cookies",
            null,
            null,
            CancellationToken.None)
            .Returns(new ProductState("Cookies", "Yummy!"));

        // Act
        ProductState? state = await handler.Handle(query, CancellationToken.None);

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