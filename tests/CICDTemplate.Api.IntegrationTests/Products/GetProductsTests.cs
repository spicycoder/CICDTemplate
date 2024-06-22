using System.Net.Http.Json;

using CICDTemplate.Api.Controllers.Products;
using CICDTemplate.Api.IntegrationTests.Infrastructure;

using FluentAssertions;

namespace CICDTemplate.Api.IntegrationTests.Products;

public class GetProductsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetProducts_HappyPath_ReturnsAllProducts()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);

        // act
        ReadProductsResponse? response = await Client.GetFromJsonAsync<ReadProductsResponse>(uri);

        // assert
        response.Should().NotBeNull();
        response!.Products.Count.Should().BeGreaterThanOrEqualTo(10);
    }
}
