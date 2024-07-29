using CICDTemplate.Application.Secrets.Queries;
using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Secrets;

public class ReadSecretQueryHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<ReadSecretQueryHandler> _logger = Substitute.For<ILogger<ReadSecretQueryHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        ReadSecretQuery query = new("Cookies");
        var handler = new ReadSecretQueryHandler(_daprClient, _logger);
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
        string? value = await handler.Handle(query, CancellationToken.None);

        // Assert
        value.Should().NotBeNull();
        value!.Should().Be("Yummy!");
    }
}