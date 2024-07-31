using System.Threading;

using Bogus;
using CICDTemplate.Application.Abstractions.Caching;
using CICDTemplate.Application.Products.Queries.ReadProducts;
using CICDTemplate.Domain.Abstract;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CICDTemplate.Application.UnitTests.ReadProducts;

public class ReadProductsQueryHandlerTests
{
    private readonly IProductsRepository _productsRepository = Substitute.For<IProductsRepository>();
    private readonly ICacheService _cacheService = Substitute.For<ICacheService>();
    private readonly ILogger<ReadProductsQueryHandler> _logger = Substitute.For<ILogger<ReadProductsQueryHandler>>();

    [Fact]
    public async Task Handle_HappyPath_ShouldReturnProducts()
    {
        // Arrange
        var products = new Faker<Product>()
            .RuleFor(x => x.Name, x => string.Concat(x.Commerce.ProductName().Take(20)))
            .RuleFor(x => x.Description, x => string.Concat(x.Commerce.ProductDescription().Take(200)))
            .RuleFor(x => x.CreatedAtUtc, x => DateTime.UtcNow.AddDays(x.Random.Number(0, 6)))
            .Generate(10)
            .ToArray();

        _productsRepository
            .GetProductsAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((IEnumerable<Product>)products));

        _cacheService
            .GetAsync<Product[]>(Constants.AllProductsCacheKey, CancellationToken.None)
            .Returns(products);

        ReadProductsQueryHandler handler = new(
            _productsRepository,
            _cacheService,
            _logger);

        // Act
        var response = await handler.Handle(new ReadProductsQuery(), CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response!.Length.Should().Be(10);

        await _productsRepository
            .Received(0)
            .GetProductsAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_NoCacheAvailable_ShouldMakeRepoCall()
    {
        // Arrange
        var products = new Faker<Product>()
            .RuleFor(x => x.Name, x => string.Concat(x.Commerce.ProductName().Take(20)))
            .RuleFor(x => x.Description, x => string.Concat(x.Commerce.ProductDescription().Take(200)))
            .RuleFor(x => x.CreatedAtUtc, x => DateTime.UtcNow.AddDays(x.Random.Number(0, 6)))
            .Generate(10)
            .ToArray();

        _productsRepository
            .GetProductsAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((IEnumerable<Product>)products));

        _cacheService
            .GetAsync<Product[]>(Constants.AllProductsCacheKey, CancellationToken.None)
            .Returns((Product[]?)null);

        ReadProductsQueryHandler handler = new(
            _productsRepository,
            _cacheService,
            _logger);

        // Act
        var response = await handler.Handle(new ReadProductsQuery(), CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response!.Length.Should().Be(10);

        await _productsRepository
            .Received(1)
            .GetProductsAsync(Arg.Any<CancellationToken>());
    }
}