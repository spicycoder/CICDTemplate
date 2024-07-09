using System.Net;
using System.Web;

using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using FluentAssertions;

using Moq;

namespace CICDTemplate.FunctionalTests.Secrets;

[Collection("App Host")]
public class SecretsControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly IntegrationTestWebAppFactory _factory;

    public SecretsControllerTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Read_HappyPath_Success()
    {
        // Arrange
        Uri uri = new($"/api/secrets/read?key={HttpUtility.UrlEncode("Cookies")}", UriKind.Relative);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.GetSecretAsync(
                Constants.SecretsStoreName,
                "Cookies",
                null,
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new Dictionary<string, string>()
            {
                { "Cookies", "Yummy!" }
            }));

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Read_KeyNotExist_ReturnNotFound()
    {
        // Arrange
        Uri uri = new($"/api/secrets/read?key={HttpUtility.UrlEncode("Cookies")}", UriKind.Relative);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.GetSecretAsync(
                Constants.SecretsStoreName,
                "Cookies",
                null,
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new Dictionary<string, string>()
            {
                { "Fries", "Yummy!" }
            }));

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}