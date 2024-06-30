using CICDTemplate.Api.Controllers.Products;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace CICDTemplate.FunctionalTests.Products;

[Collection("App Host")]
public class CreateProductsTest
{
    private readonly HttpClient _httpClient;

    public CreateProductsTest(IntegrationTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_HappyPath_ReturnsId()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest request = new("Cookies", "Yummy!");

        // act
        var response = await _httpClient.PostAsJsonAsync(uri, request);
        Guid? result = await response.Content.ReadFromJsonAsync<Guid?>();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateProduct_NullRequest_ReturnsBadRequest()
    {
        // arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest? request = null;

        // act
        var response = await _httpClient.PostAsJsonAsync(uri, request);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
