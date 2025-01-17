using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using Shouldly;
using Moq;
using System.Net;
using System.Web;

namespace CICDTemplate.FunctionalTests.Configurations;

[Collection("App Host")]
public class ConfigurationsControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly IntegrationTestWebAppFactory _factory;

    public ConfigurationsControllerTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Read_HappyPath_Success()
    {
        // Arrange
        Uri uri = new($"/api/configurations/read?name={HttpUtility.UrlEncode("Cookies")}", UriKind.Relative);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;

        daprClient.Setup(x => x.GetConfiguration(
            Constants.ConfigStoreName,
            It.IsAny<IReadOnlyList<string>>(),
            null,
            CancellationToken.None
            ))
            .Returns(Task.FromResult(new GetConfigurationResponse(new Dictionary<string, ConfigurationItem>
            {
                { "Cookies", new ConfigurationItem("Yummy!", string.Empty, null) }
            })));

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Read_NameNotExist_ReturnNotFound()
    {
        // Arrange
        Uri uri = new($"/api/configurations/read?name={HttpUtility.UrlEncode("Cookies")}", UriKind.Relative);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;

        daprClient.Setup(x => x.GetConfiguration(
            Constants.ConfigStoreName,
            It.IsAny<IReadOnlyList<string>>(),
            null,
            CancellationToken.None
            ))
            .Returns(Task.FromResult(new GetConfigurationResponse(new Dictionary<string, ConfigurationItem>
            {
                { "Fries", new ConfigurationItem("Crispy!", string.Empty, null) }
            })));

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
