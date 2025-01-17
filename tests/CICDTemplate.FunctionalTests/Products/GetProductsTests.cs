using System.Net.Http.Json;

using CICDTemplate.Api.Controllers.Products;

using Shouldly;

namespace CICDTemplate.FunctionalTests.Products;

[Collection("App Host")]
public class GetProductsTests
{
    private readonly HttpClient _httpClient;

    public GetProductsTests(IntegrationTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_HappyPath_ReturnsAllProducts()
    {
        // Arrange
        Uri uri = new("/api/products", UriKind.Relative);

        // Act
        ReadProductsResponse? response = await _httpClient.GetFromJsonAsync<ReadProductsResponse>(uri);

        // Assert
        response.ShouldNotBeNull();
        response!.Products.Count.ShouldBeGreaterThanOrEqualTo(10);
    }
}