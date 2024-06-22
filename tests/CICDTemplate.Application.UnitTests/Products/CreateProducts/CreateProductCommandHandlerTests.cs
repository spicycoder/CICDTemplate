using CICDTemplate.Application.Abstractions.Clock;
using CICDTemplate.Application.Products.Commands.CreateProduct;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using FluentAssertions;

using NSubstitute;

namespace CICDTemplate.Application.UnitTests.Products.CreateProducts;

public class CreateProductCommandHandlerTests
{
    private readonly IProductsRepository _productsRepository = Substitute.For<IProductsRepository>();
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    [Fact]
    public async Task Handle_HappyPath_ReturnsId()
    {
        // arrange
        var expectedDate = DateTime.UtcNow.AddDays(-1);
        _dateTimeProvider
            .Now
            .Returns(expectedDate);

        Guid? expectedId = Guid.NewGuid();
        _productsRepository
            .CreateProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expectedId));

        CreateProductCommandHandler handler = new(
            _productsRepository,
            _dateTimeProvider);

        // act
        var response = await handler.Handle(
            new CreateProductCommand("Cookie", "Yummy"),
            new CancellationToken());

        // assert
        response.Should().NotBeNull();
        response!.Should().Be(expectedId);
    }
}
