using CICDTemplate.Api.Controllers.Products;

using FluentAssertions;

using System.Net;
using System.Net.Http.Json;

namespace CICDTemplate.E2ETests.Products;

[Collection("App Host")]
public class CreateProductsTest(AppHostFixture fixture)
{
    [Fact]
    public async Task CreateProduct_HappyPath_ReturnsId()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest request = new("Cookies", "Yummy!");

        // act
        var response = await fixture.Client.PostAsJsonAsync(uri, request);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateProduct_NullRequest_ReturnsBadRequest()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest? request = null;

        // act
        var response = await fixture.Client.PostAsJsonAsync(uri, request);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
