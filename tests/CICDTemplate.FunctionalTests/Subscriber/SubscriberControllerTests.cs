using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

using CICDTemplate.Api.Models.Common;

using FluentAssertions;

namespace CICDTemplate.FunctionalTests.Subscriber;

[Collection("App Host")]
public class SubscriberControllerTests
{
    private readonly HttpClient _httpClient;

    public SubscriberControllerTests(IntegrationTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Subscribe_HappyPath_Success()
    {
        // Arrange
        Uri uri = new("/api/subscriber/consume", UriKind.Relative);
        Product message = new("Cookies", "Yummy");
        StringContent content = new(
            JsonSerializer.Serialize(message),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        // Act
        var response = await _httpClient.PostAsync(uri, content, CancellationToken.None);
        content.Dispose();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}