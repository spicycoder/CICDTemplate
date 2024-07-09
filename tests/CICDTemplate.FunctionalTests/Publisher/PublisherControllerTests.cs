using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

using CICDTemplate.Api.Models.Common;
using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using FluentAssertions;

using Moq;

namespace CICDTemplate.FunctionalTests.Publisher;

[Collection("App Host")]
public class PublisherControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly IntegrationTestWebAppFactory _factory;

    public PublisherControllerTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Publish_HappyPath_Success()
    {
        // Arrange
        Uri uri = new("/api/publisher/publish", UriKind.Relative);
        Product message = new("Cookies", "Yummy");
        StringContent content = new(
            JsonSerializer.Serialize(message),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.PublishEventAsync(
            Constants.PubSubName,
            Constants.PubSubTopicName,
            message,
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var response = await _httpClient.PostAsync(uri, content);
        content.Dispose();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}