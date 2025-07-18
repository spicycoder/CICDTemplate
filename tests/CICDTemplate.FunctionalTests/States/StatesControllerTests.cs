﻿using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Web;

using CICDTemplate.Application.States.Commands.SaveState;
using CICDTemplate.Application.States.Queries.ReadState;
using CICDTemplate.Domain.Abstract;

using Dapr.Client;

using Shouldly;

using Moq;

namespace CICDTemplate.FunctionalTests.States;

[Collection("App Host")]
public class StatesControllerTests
{
    private const string ProductName = "Cookies";
    private readonly HttpClient _httpClient;
    private readonly IntegrationTestWebAppFactory _factory;

    public StatesControllerTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Save_HappyPath_Success()
    {
        // Arrange
        Uri uri = new("/api/states/save", UriKind.Relative);
        SaveStateCommand command = new("Cookies", "Yummy!");
        StringContent content = new(
            JsonSerializer.Serialize(command),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.SaveStateAsync(
                Constants.StateStoreName,
                "Cookies",
                command,
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        HttpResponseMessage response = await _httpClient.PostAsync(
            uri,
            content,
            CancellationToken.None);
        content.Dispose();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Read_HappyPath_Success()
    {
        // Arrange
        Uri uri = new($"/api/states/read?productName={HttpUtility.UrlEncode(ProductName)}", UriKind.Relative);

        ProductState expected = new(ProductName, "Yummy!");

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.GetStateAsync<ProductState?>(
                Constants.StateStoreName,
                ProductName,
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult((ProductState?)expected));

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Read_NonExistentState_NotFound()
    {
        // Arrange
        Uri uri = new($"/api/states/read?productName={HttpUtility.UrlEncode(ProductName)}", UriKind.Relative);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.GetStateAsync<ProductState?>(
                Constants.StateStoreName,
                ProductName,
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult((ProductState?)null));

        // Act
        HttpResponseMessage response = await _httpClient.GetAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_HappyPath_Success()
    {
        // Arrange
        Uri uri = new($"/api/states/delete?productName={HttpUtility.UrlEncode(ProductName)}", UriKind.Relative);

        Mock<DaprClient> daprClient = _factory.MockDaprClient;
        daprClient
            .Setup(x => x.DeleteStateAsync(
                Constants.StateStoreName,
                ProductName,
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        HttpResponseMessage response = await _httpClient.DeleteAsync(uri, CancellationToken.None);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}