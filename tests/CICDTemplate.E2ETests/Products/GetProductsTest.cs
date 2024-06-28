using CICDTemplate.Api.Controllers.Products;

using FluentAssertions;
using System.Net.Http.Json;

namespace CICDTemplate.E2ETests.Products;

[Collection("App Host")]
public class GetProductsTest(AppHostFixture fixture)
{
    [Fact]
    public async Task GetProducts_HappyPath_ReturnsAllProducts()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);

        // act
        ReadProductsResponse? response = await fixture.Client.GetFromJsonAsync<ReadProductsResponse>(uri);

        // assert
        response.Should().NotBeNull();
        response!.Products.Count.Should().BeGreaterThanOrEqualTo(10);
    }
}
