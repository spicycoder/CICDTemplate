using CICDTemplate.Application.Configurations.Queries;
using CICDTemplate.Application.Secrets.Queries;
using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using Shouldly;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CICDTemplate.Application.UnitTests.ReadConfiguration;

public class ReadConfigurationQueryHandlerTests
{
    private readonly DaprClient _daprClient = Substitute.For<DaprClient>();
    private readonly ILogger<ReadConfigurationQueryHandler> _logger = Substitute.For<ILogger<ReadConfigurationQueryHandler>>();

    [Fact]
    public async Task Handle_HappyPath_Success()
    {
        // Arrange
        ReadConfigurationQuery query = new("Cookies");

        var handler = new ReadConfigurationQueryHandler(_daprClient, _logger);

        _daprClient
            .GetConfiguration(
            Constants.ConfigStoreName,
            Arg.Any<IReadOnlyList<string>>(),
            null,
            CancellationToken.None)
            .Returns(Task.FromResult(new GetConfigurationResponse(new Dictionary<string, ConfigurationItem>
            {
                { "Cookies", new ConfigurationItem("Yummy!", string.Empty, null) }
            })));

        // Act
        string? value = await handler.Handle(query, CancellationToken.None);

        // Assert
        value.ShouldNotBeNull();
        value!.ShouldBe("Yummy!");
    }
}