using Bogus;

using CICDTemplate.Application.Products.Queries;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Products.ReadProducts;

public class ReadProductsQueryHandlerTests
{
    private readonly IProductsRepository _productsRepository = Substitute.For<IProductsRepository>();
    private readonly ILogger<ReadProductsQueryHandler> _logger = Substitute.For<ILogger<ReadProductsQueryHandler>>();

    [Fact]
    public async Task Handle_HappyPath_ShouldReturnProducts()
    {
        // arrange
        var products = new Faker<Product>()
            .RuleFor(x => x.Name, x => string.Concat(x.Commerce.ProductName().Take(20)))
            .RuleFor(x => x.Description, x => string.Concat(x.Commerce.ProductDescription().Take(200)))
            .RuleFor(x => x.CreatedAtUtc, x => DateTime.UtcNow.AddDays(x.Random.Number(0, 6)))
            .Generate(10);

        _productsRepository
            .GetProductsAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((IEnumerable<Product>)products));

        ReadProductsQueryHandler handler = new(_productsRepository, _logger);

        // act
        var response = await handler.Handle(new ReadProductsQuery(), CancellationToken.None);

        // assert
        response.Should().NotBeNull();
        response!.Length.Should().Be(10);
    }
}
