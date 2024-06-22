using CICDTemplate.Api.Controllers.Products;
using System.Net.Http.Json;

using CICDTemplate.Api.IntegrationTests.Infrastructure;

using FluentAssertions;
using System.Net;

namespace CICDTemplate.Api.IntegrationTests.Products;

public class CreateProductTest(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateProduct_HappyPath_ReturnsId()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest request = new("Cookies", "Yummy!");

        // act
        var response = await Client.PostAsJsonAsync(uri, request);

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
        var response = await Client.PostAsJsonAsync(uri, request);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
