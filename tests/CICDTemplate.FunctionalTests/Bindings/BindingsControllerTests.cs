using System.Net;

using FluentAssertions;

namespace CICDTemplate.FunctionalTests.Bindings;

[Collection("App Host")]
public class BindingsControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly IntegrationTestWebAppFactory _factory;

    public BindingsControllerTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Cron_HappyPath_Success()
    {
        // Arrange
        Uri uri = new("/api/bindings/cron", UriKind.Relative);

        // Act
        var response = await _httpClient.PostAsync(uri, null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}