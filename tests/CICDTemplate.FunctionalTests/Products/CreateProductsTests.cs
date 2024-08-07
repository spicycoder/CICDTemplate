﻿using System.Net;
using System.Net.Http.Json;

using CICDTemplate.Api.Controllers.Products;

using FluentAssertions;

namespace CICDTemplate.FunctionalTests.Products;

[Collection("App Host")]
public class CreateProductsTests
{
    private readonly HttpClient _httpClient;

    public CreateProductsTests(IntegrationTestWebAppFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_HappyPath_ReturnsId()
    {
        // Arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest request = new("Cookies", "Yummy!");

        // Act
        var response = await _httpClient.PostAsJsonAsync(uri, request);
        Guid? result = await response.Content.ReadFromJsonAsync<Guid?>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateProduct_NullRequest_ReturnsBadRequest()
    {
        // Arrange
        Uri uri = new("/api/products", UriKind.Relative);
        CreateProductRequest? request = null;

        // Act
        var response = await _httpClient.PostAsJsonAsync(uri, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}