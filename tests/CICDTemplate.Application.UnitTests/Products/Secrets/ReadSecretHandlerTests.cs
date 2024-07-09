using CICDTemplate.Application.Secrets.Queries;
using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Products.Secrets;

public class ReadSecretHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<ReadSecretHandler> _logger = Substitute.For<ILogger<ReadSecretHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        ReadSecretCommand command = new("Cookies");
        var handler = new ReadSecretHandler(_daprClient, _logger);
        _daprClient
            .GetSecretAsync(
            Constants.SecretsStoreName,
            "Cookies",
            null,
            CancellationToken.None)
            .Returns(Task.FromResult(new Dictionary<string, string>
            {
                { "Cookies", "Yummy!" }
            }));

        // Act
        string? value = await handler.Handle(command, CancellationToken.None);

        // Assert
        value.Should().NotBeNull();
        value!.Should().Be("Yummy!");
    }
}