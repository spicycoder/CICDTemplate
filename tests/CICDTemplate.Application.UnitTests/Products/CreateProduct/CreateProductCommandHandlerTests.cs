using CICDTemplate.Application.Abstractions.Clock;
using CICDTemplate.Application.Products.Commands.CreateProduct;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using FluentAssertions;

using Moq;

namespace CICDTemplate.Application.UnitTests.Products.CreateProduct;

public class CreateProductCommandHandlerTests
{
    private Mock<IProductsRepository> _mockProductsRepository;
    private Mock<IDateTimeProvider> _mockDateTimeProvider;

    [SetUp]
    public void Setup()
    {
        _mockProductsRepository = new Mock<IProductsRepository>();
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
    }

    [Test]
    public async Task Handle_HappyPath_ReturnsId()
    {
        // arrange
        var expectedDate = DateTime.UtcNow.AddDays(-1);
        _mockDateTimeProvider
            .Setup(x => x.Now)
            .Returns(expectedDate);

        Guid? expectedId = Guid.NewGuid();
        _mockProductsRepository
            .Setup(x => x.CreateProductAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedId));

        CreateProductCommandHandler handler = new(
            _mockProductsRepository.Object,
            _mockDateTimeProvider.Object);

        // act
        var response = await handler.Handle(
            new CreateProductCommand("Cookie", "Yummy"),
            new CancellationToken())
            .ConfigureAwait(false);

        // assert
        response.Should().NotBeNull();
        response!.Should().Be(expectedId);
    }
}
