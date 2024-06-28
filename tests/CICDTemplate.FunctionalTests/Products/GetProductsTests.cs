using System.Net.Http.Json;

using CICDTemplate.Api.Controllers.Products;

using FluentAssertions;

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
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);

        // act
        ReadProductsResponse? response = await _httpClient.GetFromJsonAsync<ReadProductsResponse>(uri);

        // assert
        response.Should().NotBeNull();
        response!.Products.Count.Should().BeGreaterThanOrEqualTo(10);
    }
}
